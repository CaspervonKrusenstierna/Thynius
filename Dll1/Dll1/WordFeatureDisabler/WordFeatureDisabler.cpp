#include "WordFeatureDisabler.h"

_CpDoReplace pCpDoReplace = nullptr;
PVOID pCpDoReplaceTarget;
void __fastcall detourCpDoReplace(__int64, __int64, __int64, int*) {

}

FInsertTable pInsertTable = nullptr;
PVOID pInsertTableTarget;
void __fastcall detourFInsertTable(int, int) {

}
WordFeatureDisabler::WordFeatureDisabler() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	modBase = (char*)ModInfo.lpBaseOfDll;
}

bool WordFeatureDisabler::disableReplace() {
	PVOID PatternAddy = PatternScan(this->modBase, "45 33 FF 89 54 24 50 44 89 7D D0 8B FA 44 8B F1 4C 89 4D 10");
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pCpDoReplaceTarget = PVOID((DWORD64)PatternAddy - 0x3E);

	if (MH_CreateHook(pCpDoReplaceTarget, &detourCpDoReplace, (LPVOID*)&pCpDoReplace) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	return true;
}

bool WordFeatureDisabler::disableInsertTable() {
	PVOID PatternAddy = PatternScan(this->modBase, "44 8D 4B 08 E8 ? ? ? ? 85 C0 75 60 45 33 C0 48 8D 4C 24 ?");
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pInsertTableTarget = PVOID((DWORD64)PatternAddy - 0x44);

	if (MH_CreateHook(pInsertTableTarget, &detourFInsertTable, (LPVOID*)&pInsertTableTarget) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	return true;
}