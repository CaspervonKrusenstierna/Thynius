
#include "Comms.h"


#define BUF_SIZE 256
TCHAR szMsg[] = TEXT("Message from first process.");

Comms::Comms() {
    this->hMapFile = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, L"Global\\ThemisIPC");

    if (hMapFile == NULL)
    {
        return;
    }

    this->pBuf = (LPTSTR)MapViewOfFile(hMapFile,   // handle to map object
        FILE_MAP_ALL_ACCESS, // read/write permission
        0,
        0,
        BUF_SIZE);

    if (pBuf == NULL)
    {
        CloseHandle(hMapFile);
        return;
    }
    return;
}
Comms::~Comms() {
    CloseHandle(this->PipeHandle);
    UnmapViewOfFile(pBuf);

    CloseHandle(hMapFile);
}
void Comms::SendMessageW(const wchar_t* szMsg) {
    CopyMemory((PVOID)pBuf, szMsg, (_tcslen(szMsg) * sizeof(TCHAR)));
}