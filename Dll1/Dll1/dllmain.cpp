

#include "Comms/Comms.h"
#include "Hooks/Hooks.h"
#include "Data/Data.h"
#include "ThemisSessionData/ThemisSessionData.h"

Comms* comms;
Hooks* hooks;
ThemisSessionData* themisSessionData;
Data* data;

void OnTextInputFunc(wchar_t input) {
    if (_wcsnicmp(reinterpret_cast<const wchar_t*>(input), L"8", 1)) {
        themisSessionData->LogInput(ActionType::DELETESELECTION, L"", data->GetSelection());
    }
    else {
        themisSessionData->LogInput(ActionType::ADDCHAR, &input, data->GetSelection());
    }
}
void OnPasteFunc(std::wstring PastedText){
    themisSessionData->LogInput(ActionType::PASTE, PastedText, data->GetSelection());
}

DWORD WINAPI MainThread(HMODULE hModule){
    comms = new Comms();
    hooks = new Hooks();
    data = new Data();
    themisSessionData = new ThemisSessionData();

    hooks->HookOnTextInput(OnTextInputFunc);
    hooks->HookOnPasteText(OnPasteFunc);

    while (true) {

        Sleep(1000);
    }
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

