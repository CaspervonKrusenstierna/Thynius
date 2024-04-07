#pragma once
#include <Windows.h>
#include "../MemUtils/MemUtils.h"
#include "../Hooks//Hooks.h"
#include <fstream>
#include <iostream>
#include <string>

//offsets from wwlib.dll
enum Offsets {
	CHARCOUNT = 0x29846A0
};

struct Selection {
	UINT32 SelectionStart;
	UINT32 SelectionEnd;
};

class Data {
public:
	Data();
	Selection GetSelection();
	UINT64 GetCp();
private:
	PVOID selAddy;
	INT32 CpRODOffset;
};