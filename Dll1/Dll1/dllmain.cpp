

#include "Comms/Comms.h"
#include "Hooks/Hooks.h"
#include "Data/Data.h"
#include "WordFeatureDisabler/WordFeatureDisabler.h"
#include "ThemisSessionData/ThemisSessionData.h"
#pragma comment(lib,"shlwapi.lib")
#include "shlobj.h"

char path[MAX_PATH];

Comms* comms;
Hooks* hooks;
ThemisSessionData* themisSessionData;
Data* data;
WordFeatureDisabler* wordFeatureDisabler;

std::wofstream DebugOutput("C:\\Program Files\\Themis\\DebugMain");

void OnTextInputFunc(wchar_t input) {
    if (input == 8) {
        themisSessionData->LogInput(ActionType::DELETESELECTION, L"", data->GetSelection());
    }
    else {
        themisSessionData->LogInput(ActionType::ADDCHAR, std::wstring(&input, 1), data->GetSelection());
    }
}
void OnPasteFunc(std::wstring PastedText){
    themisSessionData->LogInput(ActionType::PASTE, PastedText, data->GetSelection());
}

UINT64 stashedUndoValue = 0;
bool OnUndoFunc(UINT64 isUndo) {
    ActionType lastActionType = themisSessionData->GetLastActionType();
    if (lastActionType != ActionType::PASTE && lastActionType != ActionType::DELETESELECTION && lastActionType != ActionType::UNDO) {
        return false;
    }
    if (stashedUndoValue == 0 || _abs64(stashedUndoValue - isUndo) >= 2) {
        stashedUndoValue = isUndo;
    }
    if (stashedUndoValue == isUndo) {
        themisSessionData->LogInput(ActionType::UNDO, L"", data->GetSelection());
    }
    else {
        if (lastActionType != ActionType::UNDO) {
            return false;
        }
        themisSessionData->PopInput();
    }
}

void OnSaveFunc(unsigned long GUID) {
    SessionData sessionData = themisSessionData->GetSessionData();
    std::wofstream Output(path + std::string("\\Themis\\Sessions\\") + std::to_string(GUID));
    std::wofstream MetaOutput(path + std::string("\\Themis\\Sessions\\") + std::to_string(GUID) + "_metadata");

    for (int i = 0; sessionData.inputs.size() > i; i++) {
        Input currInput = sessionData.inputs[i];
        
        Output << L'"' << currInput.ActionContent << L'"' << L",";
        Output << currInput._ActionType << ',';
        Output << currInput._Selection.SelectionStart << ',';
        Output << currInput._Selection.SelectionEnd << ',';
        Output << currInput.Cp << ',';
        Output << currInput.relativeTimePointMs << '\n';
    }

    MetaOutput << sessionData.endCp;
    Output.close();
    MetaOutput.close();
}

void onSpellingReplace(std::wstring replacement, UINT32 replacementStart, UINT32 replacementEnd) {
    Selection toAdd;
    toAdd.SelectionStart = replacementStart;
    toAdd.SelectionEnd = replacementEnd;
    themisSessionData->LogInput(ActionType::SPELLINGREPLACE, replacement, toAdd);
}

DWORD WINAPI MainThread(HMODULE hModule){
    std::chrono::system_clock::time_point startTime = std::chrono::system_clock::now();
   SHGetFolderPathA(NULL, CSIDL_COMMON_APPDATA, NULL, 0, path);

    comms = new Comms();
    hooks = new Hooks();
    wordFeatureDisabler = new WordFeatureDisabler();
    data = new Data();
    themisSessionData = new ThemisSessionData(data, startTime);
    themisSessionData->LogSessionStart(startTime);

    if (!hooks->HookOnPasteText(OnPasteFunc)) {
        DebugOutput << "Fatal error: Failed to hook paste" << std::endl;
    }
    if (!hooks->HookOnTextInput(OnTextInputFunc)) {
        DebugOutput << "Fatal error: Failed to hook textinput" << std::endl;
    }
    if (!hooks->HookSave(OnSaveFunc)) {
        DebugOutput << "Fatal error: Failed to hook save" << std::endl;
    }
    if (!hooks->HookUndo(OnUndoFunc)) {
        DebugOutput << "Fatal error: Failed to hook undo" << std::endl;
    }
    if (!hooks->HookDoWordReplace(onSpellingReplace)) {
        DebugOutput << "Fatal error: Failed to hook doWordReplace" << std::endl;
    }

    if (!wordFeatureDisabler->disableReplace()) {
        DebugOutput << "Error: Failed to disable replace" << std::endl;
    }
    if (!wordFeatureDisabler->disableInsertTable()) {
        DebugOutput << "Error: Failed to disable insertTable" << std::endl;
    }
    return 0;
}
BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        CloseHandle(CreateThread(nullptr, 0, (LPTHREAD_START_ROUTINE)MainThread, hModule, 0, nullptr));
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

