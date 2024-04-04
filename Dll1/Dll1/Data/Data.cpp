#include "Data.h"

// ptr points to a unicode string structured something like this: "5 words"
Data::Data() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	char* modBase = (char*)ModInfo.lpBaseOfDll;
	PVOID SelectionPattern = PatternScan(modBase, "83 3d ? ? ? ? ? 74 ? 48 8b 05 ? ? ? ? 48 89 05");
	SelectionStartAddy = reinterpret_cast<uint32_t*>(*reinterpret_cast<uint32_t*>(reinterpret_cast<DWORD64>(SelectionPattern) + 0x17 + 0x2) + reinterpret_cast<DWORD64>(SelectionPattern) + 0x17 + 0x2 + 0x4);
	SelectionEndAddy = reinterpret_cast<uint32_t*>(*reinterpret_cast<uint32_t*>(reinterpret_cast<DWORD64>(SelectionPattern) + 0x17 + 0x8) + reinterpret_cast<DWORD64>(SelectionPattern) + 0x17 + 0x8 + 0x4);
}

Selection Data::GetSelection() {
	Selection toReturn;
	toReturn.SelectionStart = *SelectionStartAddy;
	toReturn.SelectionEnd = *SelectionEndAddy;
	return toReturn;
}