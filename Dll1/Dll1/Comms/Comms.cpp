
#include "Comms.h"




Comms::Comms() {
    DWORD dwMode;
    BOOL fSuccess;
    while(1)
    {
        this->PipeHandle = CreateFile(
            lpszPipename,   // pipe name 
            GENERIC_READ |  // read and write access 
            GENERIC_WRITE,
            0,              // no sharing 
            NULL,           // default security attributes
            OPEN_EXISTING,  // opens existing pipe 
            0,              // default attributes 
            NULL);          // no template file 

        // Break if the pipe handle is valid. 
        if (this->PipeHandle != INVALID_HANDLE_VALUE)
            break;

        // Exit if an error other than ERROR_PIPE_BUSY occurs. 
        if (GetLastError() != ERROR_PIPE_BUSY)
        {
            return;
        }

        // All pipe instances are busy, so wait for 20 seconds. 
        if (!WaitNamedPipe(lpszPipename, 20000))
        {
            return;
        }
    }
    dwMode = PIPE_READMODE_MESSAGE;
    fSuccess = SetNamedPipeHandleState(
        PipeHandle,    // pipe handle 
        &dwMode,  // new pipe mode 
        NULL,     // don't set maximum bytes 
        NULL);    // don't set maximum time 
}
Comms::~Comms() {
    CloseHandle(this->PipeHandle);
}
DWORD Comms::SendMessage(std::wstring szMsg) {
    std::wstring stringOne = szMsg;
    std::wstring stringTwo = L"AAAA";
    std::wstring combined = (stringTwo + stringOne + stringTwo);

    TCHAR chReadBuf[BUF_SIZE];
    DWORD cbResult;
    
    bool result = WriteFile(
        this->PipeHandle,                  // pipe handle 
        combined.c_str(),              // message to server
        (wcslen(combined.c_str() + 1)) * sizeof(wchar_t), // message length 
        &cbResult,              // buffer to receive reply
        NULL);

    return cbResult;
}