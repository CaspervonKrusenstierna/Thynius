#pragma once
#include "../MemUtils/MemUtils.h"
#pragma comment(lib, "libMinHook.x64.lib")
#include <MinHook.h>

typedef void(__fastcall* _CpDoReplace)(__int64, __int64, __int64, int*);
typedef void(__fastcall* FInsertTable)(int, int);

class WordFeatureDisabler {
	public:
		WordFeatureDisabler();

		bool disableReplace();
		bool disableInsertTable();
	private:
		char* modBase;
};