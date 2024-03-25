#pragma once
#include <Windows.h>
#include "../MemUtils/MemUtils.h"

//offsets from wwlib.dll
enum Offsets {
	SELECTIONSTART = 0x29C4D28,
	SELECTIONEND = 0x29C4D2C
};

struct Selection {
	UINT32 SelectionStart;
	UINT32 SelectionEnd;
};
class Data {
public:
	Data();
	Selection GetSelection();
private:
	UINT32* SelectionStartAddy;
	UINT32* SelectionEndAddy;
};