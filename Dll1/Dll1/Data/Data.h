#pragma once
#include <Windows.h>
#include "../MemUtils/MemUtils.h"
#include <fstream>
#include <iostream>
#include <string>

//offsets from wwlib.dll
enum Offsets {
	SELECTIONSTART = 0x29C4D28,
	SELECTIONEND = 0x29C4D2C,
	CHARCOUNT = 0x29846A0
};

struct Selection {
	UINT32 SelectionStart;
	UINT32 SelectionEnd;
};
struct StartUpIdentifierData {
	UINT32 CharCount;
	UINT32 WordCount;
};
class Data {
public:
	Data();
	Selection GetSelection();
	UINT32 GetWordCount();
	UINT32 GetCharCount();
private:
	UINT32* SelectionStartAddy;
	UINT32* SelectionEndAddy;

	StartUpIdentifierData IdentityData;
};