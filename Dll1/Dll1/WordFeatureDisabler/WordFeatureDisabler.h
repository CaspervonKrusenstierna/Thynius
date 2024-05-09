#pragma once
#include "../MemUtils/MemUtils.h"
#include <MinHook.h>

#if _WIN64 
	#pragma comment(lib, "libMinHook.x64.lib")
#else
	#pragma comment(lib, "libMinHook.x86.lib")
#endif
#if _WIN64 
#pragma comment(lib, "libMinHook.x64.lib")
#else
#pragma comment(lib, "libMinHook.x86.lib")
#endif
#if _WIN32 || _WIN64
#if _WIN64
#define ENV64BIT
#else
#define ENV32BIT
#endif
#endif

// Check GCC
#if __GNUC__
#if __x86_64__ || __ppc64__
#define ENV64BIT
#else
#define ENV32BIT
#endif
#endif

#if defined(ENV64BIT)
typedef void(__fastcall* _CpDoReplace)(__int64, __int64, __int64, int*);
#else
typedef long(__stdcall* _CpDoReplace)(long, long, int, int*);
#endif


class WordFeatureDisabler {
	public:
		WordFeatureDisabler();

		bool disableReplace();
	private:
		char* modBase;
};