#include "WordFeatureDisabler.h"

_CpDoReplace pCpDoReplace = nullptr;
PVOID pCpDoReplaceTarget;
#if defined(ENV64BIT)
void __fastcall detourCpDoReplace(__int64, __int64, __int64, int*) {
}
#else
long __fastcall detourCpDoReplace(long, long, int, int*) {
	return 0;
}
#endif

WordFeatureDisabler::WordFeatureDisabler() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	modBase = (char*)ModInfo.lpBaseOfDll;
}


bool WordFeatureDisabler::disableReplace() {
	#if defined(ENV64BIT)
	    PVOID PatternAddy = PatternScan(this->modBase, "45 33 FF 89 54 24 50 44 89 7D D0 8B FA 44 8B F1 4C 89 4D 10");
	#else
	    PVOID PatternAddy = PatternScan(this->modBase, "8B F2 89 B5 ? ? ? ? 89 B5 ? ? ? ? 8B F9 89 BD ? ? ? ?");
	#endif

	if (PatternAddy == NULL) {
		return FALSE;
	}

	#if defined(ENV64BIT)
	    pCpDoReplaceTarget = PVOID((DWORD64)PatternAddy - 0x3E);
	#else
	    pCpDoReplaceTarget = PVOID((DWORD64)PatternAddy - 0x2B);
	#endif

	if (MH_CreateHook(pCpDoReplaceTarget, &detourCpDoReplace, (LPVOID*)&pCpDoReplace) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	return true;
}
