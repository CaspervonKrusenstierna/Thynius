#include "Data.h"

// ptr points to a unicode string structured something like this: "5 words"
std::wofstream Output("C:\\Program Files\\Themis\\DebugData");
Data::Data() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	char* modBase = (char*)ModInfo.lpBaseOfDll;
	PVOID SelectionPattern = PatternScan(modBase, "0f 10 35 ? ? ? ? 66 0f 6f c6");
	selAddy = reinterpret_cast<uint32_t*>(*reinterpret_cast<uint32_t*>(reinterpret_cast<DWORD64>(SelectionPattern) + 0x3) + reinterpret_cast<DWORD64>(SelectionPattern) + 0x7);

	PVOID CpRODOffsetPattern = PatternScan(modBase, "44 39 11 74 ? 45 8b ce 44 8b c7 8b d3 e8 ? ? ? ? ba");
    CpRODOffset = *(reinterpret_cast<INT32*>(reinterpret_cast<DWORD64>(CpRODOffsetPattern) - 0x4));
}

UINT64 Data::GetCp() {
	return GetCpCount(this->CpRODOffset);
}
Selection Data::GetSelection() {
	Selection toReturn;
	toReturn.SelectionStart = *reinterpret_cast<UINT32*>(selAddy);
	toReturn.SelectionEnd = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(selAddy) + 0x4);
	return toReturn;
}

