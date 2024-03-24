#include "Data.h"

Data::Data() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	PVOID modBase = (char*)ModInfo.lpBaseOfDll;
	SelectionStartAddy = reinterpret_cast<UINT32*>((reinterpret_cast<DWORD64>(modBase) + Offsets::SELECTIONSTART));
	SelectionEndAddy = reinterpret_cast<UINT32*>((reinterpret_cast<DWORD64>(modBase) + Offsets::SELECTIONEND));
}

Selection Data::GetSelection() {
	Selection toReturn;
	toReturn.SelectionStart = *SelectionStartAddy;
	toReturn.SelectionEnd = *SelectionEndAddy;
	return toReturn;
}