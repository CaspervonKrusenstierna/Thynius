

#include "Comms/Comms.h"
#include "Hooks/Hooks.h"
#include "Data/Data.h"
#include "ThemisSessionData/ThemisSessionData.h"
#include "./Nlohmann/json.hpp"

Comms* comms;
Hooks* hooks;
ThemisSessionData* themisSessionData;
Data* data;

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

int count = 0;
void OnSaveFunc() {
    std::vector<Input> inputs = themisSessionData->GetSessionData().SessionInputs;

    nlohmann::json result;
    for (int i = 0; inputs.size() > i; i++) {
        Input currInput = inputs[i];
        nlohmann::json j;
        j["ActionContent"] = currInput.ActionContent;
        j["ActionType"] = currInput._ActionType;
        j["Selection"] = { currInput._Selection.SelectionStart, currInput._Selection.SelectionEnd };
        j["RelativeTimeMs"] = currInput.relativeTimePointMs;
        result.push_back(j);
    }
    std::ofstream myfile;
    myfile.open("C:\\Program Files\\Themis\\Data.txt");
    myfile << result;
    myfile.close();
    std::wstring SaveMessage = L"Save";
    comms->SendMessageW((SaveMessage + std::to_wstring(count)).c_str());
}

DWORD WINAPI MainThread(HMODULE hModule){
    comms = new Comms();
    hooks = new Hooks();
    data = new Data();
    themisSessionData = new ThemisSessionData();

    hooks->HookOnPasteText(OnPasteFunc);
    hooks->HookOnTextInput(OnTextInputFunc);
    hooks->HookSave(OnSaveFunc);

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

