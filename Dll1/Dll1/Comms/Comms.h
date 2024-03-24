#include <Windows.h>
#include <stdio.h>
#include <conio.h>
#include <tchar.h>
#include <iostream>

#define BUF_SIZE 512

class Comms {
private:
	const wchar_t* lpszPipename = L"\\\\.\\pipe\\mynamedpipe";
	HANDLE PipeHandle;
public:
	Comms();
	~Comms();
	DWORD SendMessage(std::wstring szMsg);
};
HANDLE OpenPipe();