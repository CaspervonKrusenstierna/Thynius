#pragma once
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <tchar.h>
#include <iostream>
#include <fstream>

#define BUF_SIZE 512

class Comms {
private:
	const wchar_t* lpszPipename = L"\\\\.\\pipe\\mynamedpipe";
	HANDLE PipeHandle;
public:
	Comms();
	~Comms();
	LPTSTR pBuf;
	HANDLE hMapFile;
	void SendMessageW(const wchar_t* szMsg);
};
HANDLE OpenPipe();