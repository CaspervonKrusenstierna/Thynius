
#include "Hooks/Hooks.h"
#include "Data/Data.h"
#include "WordFeatureDisabler/WordFeatureDisabler.h"
#include "ThemisSessionData/ThemisSessionData.h"
#pragma comment(lib,"shlwapi.lib")
#include "shlobj.h"

std::wofstream DebugOutput("C:\\ProgramData\\Thynius\\Debug");
char path[MAX_PATH];

Hooks* hooks;
ThemisSessionData* themisSessionData;
Data* data;
WordFeatureDisabler* wordFeatureDisabler;

void OnTextInputFunc(wchar_t input) {
    DebugOutput << std::to_wstring(input) << std::endl;
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
    if (stashedUndoValue == 0 || _abs64(stashedUndoValue - isUndo) >= 2) {
        stashedUndoValue = isUndo;
    }
    if (stashedUndoValue == isUndo) {
        themisSessionData->LogInput(ActionType::UNDO, L"", data->GetSelection());
    }
    else {
        themisSessionData->LogInput(ActionType::REDO, L"", data->GetSelection());
    }
    return true;
}

void OnImageInsertFunc() {
    themisSessionData->LogInput(ActionType::INSERTIMAGE, L"", data->GetSelection());
}

void OnSaveFunc(unsigned long GUID) {
    SessionData sessionData = themisSessionData->GetSessionData();
    std::wofstream Output(path + std::string("\\Thynius\\Sessions\\") + std::to_string(GUID));
    std::wofstream MetaOutput(path + std::string("\\Thynius\\Sessions\\") + std::to_string(GUID) + "_metadata");
   
    for (int i = 0; sessionData.inputs.size() > i; i++) {
        Input currInput = sessionData.inputs[i];
        
        Output << L'\uf0e1' << currInput.ActionContent << L'\uf0e1' << L",";
        Output << currInput._ActionType << ',';
        Output << currInput._Selection.SelectionStart << ',';
        Output << currInput._Selection.SelectionEnd << ',';
        Output << currInput.MainCp << ',';
        Output << currInput.HeaderCp << ',';
        Output << currInput.FootnoteCp << ',';
        Output << currInput.relativeTimePointMs << '\n';
    }
   

    MetaOutput << sessionData.endMainCp << L"," << sessionData.endHeaderCp << L"," << sessionData.endFootnoteCp;
    Output.close();
    MetaOutput.close();
}

void onSpellingReplace(std::wstring replacement, UINT32 replacementStart, UINT32 replacementEnd) {
    Selection toAdd;
    toAdd.SelectionStart = replacementStart;
    toAdd.SelectionEnd = replacementEnd;
    themisSessionData->LogInput(ActionType::SPELLINGREPLACE, replacement, toAdd);
}
void onInsertFootnote() {
    themisSessionData->LogInput(ActionType::INSERTFOOTNOTE, L"", data->GetSelection());
}

void onInsertBreak() {
    themisSessionData->LogInput(ActionType::INSERTBREAK, L"", data->GetSelection());
}


DWORD WINAPI MainThread(HMODULE hModule){

    std::chrono::system_clock::time_point startTime = std::chrono::system_clock::now();
    SHGetFolderPathA(NULL, CSIDL_COMMON_APPDATA, NULL, 0, path);
    MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
    while (ModInfo.lpBaseOfDll == 0) {
        ModInfo = GetModuleInfo(L"wwlib.dll");
        Sleep(10);
    }
    data = new Data();
    themisSessionData = new ThemisSessionData(data, startTime);
    hooks = new Hooks(data);
    wordFeatureDisabler = new WordFeatureDisabler();
    
    if (!hooks->HookOnPasteText(OnPasteFunc)) {
       DebugOutput << "Fatal error: Failed to hook paste" << std::endl;
    }
    if (!hooks->HookOnTextInput(OnTextInputFunc)) {
        DebugOutput << "Fatal error: Failed to hook textinput" << std::endl;
    }
    if (!hooks->HookUndo(OnUndoFunc)) {
        DebugOutput << "Fatal error: Failed to hook undo" << std::endl;
    }
    if (!hooks->HookDoWordReplace(onSpellingReplace)) {
        DebugOutput << "Fatal error: Failed to hook doWordReplace" << std::endl;
    }
    if (!hooks->HookOnInsertBreak(onInsertBreak)) {
        DebugOutput << "Fatal error: Failed to hook onInsertBreak" << std::endl;
    }
    if (!hooks->HookOnImageInput(OnImageInsertFunc)) {
        DebugOutput << "Fatal error: Failed to hook onImageInsert" << std::endl;
    }
    if (!hooks->HookOnInsertFootnote(onInsertFootnote)) {
        DebugOutput << "Fatal error: Failed to hook onInsertFootnote" << std::endl;
    }
    if (!wordFeatureDisabler->disableReplace()) {
        DebugOutput << "Error: Failed to disable replace" << std::endl;
    }
    if (!hooks->HookSave(OnSaveFunc)) {
        DebugOutput << "Fatal error: Failed to hook save" << std::endl;
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

