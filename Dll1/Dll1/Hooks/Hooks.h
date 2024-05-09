#pragma once
#include <iostream>
#include <Windows.h>
#include "../SystemUtils/SystemUtils.h"
#include "../MemUtils/MemUtils.h"
#include "../Data/Data.h"

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

#include <fstream>
#include <MinHook.h>
#include <string>

#if defined(ENV64BIT)
    typedef enum CMD(__cdecl* CmdPasteCf)(struct IDataObject*, struct _SEL*, unsigned short, struct CA*, enum TBPK, unsigned short, int, unsigned short*, int*, int);
#else
    typedef int* (__fastcall* CmdPasteCf)(char* a1, int a2, __int16 a3, char* a4, int a5, __int16 a6, int a7, int a8, int a9, int a10);
#endif

typedef void(__cdecl* PasteHookCallback)(std::wstring ClipboardContent);

#if defined(ENV64BIT)
typedef void(__cdecl* CmdInsertBreakXchCore)(__int64 a1, wchar_t a2, unsigned __int16 a3, int a4, __int64 a5, char a6);
#else
typedef int(__fastcall* CmdInsertBreakXchCore)(int a1, __int16 a2, unsigned __int16 a3, int a4, int a5, char a6);
#endif
typedef void(cdecl* InsertBreakCallback)();

#if defined(ENV64BIT)
	typedef void(__thiscall* InsertXch)(PVOID, wchar_t);
#else
	typedef int(__thiscall* InsertXch)(PVOID p1, wchar_t Input);
#endif
typedef void(__cdecl* InsertHookCallback)(wchar_t);

#if defined(ENV64BIT)
typedef unsigned long(__cdecl* EventWriteEvtWordSaveStart)(void const*, void const*, int, wchar_t const*, struct _GUID const*, int, struct _GUID const*, int, int, int, int, int, int, int, unsigned int, int, int, int, unsigned int, unsigned int);
#else
typedef PVOID(__fastcall* EventWriteEvtWordSaveStart)(PVOID, PVOID, int, const wchar_t*, const struct _GUID*, int, const struct _GUID*, int, int, int, int, int, int, int, unsigned int, int, int, int, unsigned int, unsigned int);
#endif
typedef void(__cdecl* SaveCallback)(unsigned long GUID);

#if defined(ENV64BIT)
typedef __int64 (__fastcall* ItapDocCp)(PVOID a1, int a2);
#else
typedef int (__fastcall* ItapDocCp)(PVOID a1, int a2);
#endif

#if defined(ENV64BIT)
    typedef void(__fastcall* FUndoCore)(PVOID isUndo, struct DOD* a2, int a3, int a4);
#else
    typedef bool(__stdcall* FUndoCore)(bool isUndo, PVOID a2, int a3, int a4);
#endif
typedef bool(__cdecl* UndoCallback)(UINT64 isUndo);

#if defined(ENV64BIT)
typedef __int64(__fastcall* DoWordReplace)(const struct WWD* a1, unsigned int* a2, const struct DOD** dod, wchar_t* replacement, DWORD* a5, int a6, char a7, unsigned __int8 a8, __int64 a9);
#else
typedef int(__fastcall* DoWordReplace)(int a1, DWORD* a2, int* a3, wchar_t* a4, DWORD* a5, int a6, char a7, int a8, int a9, int a10);
#endif
typedef void(__cdecl* DoWordReplaceCallback)(std::wstring replacement, UINT32 replacementStart, UINT32 replacementEnd);

#if defined(ENV64BIT)
typedef void(__fastcall* InsertPictureFiles)(PVOID a1, PVOID* a2, PVOID a3, PVOID* a4, PVOID* a5, PVOID* a6);
#else
typedef void(__thiscall* InsertPictureFiles)(PVOID a1, PVOID a2, PVOID a3, PVOID a4, PVOID a5, PVOID a6, PVOID a7);
#endif
typedef void(__cdecl* InsertPictureFilesCallback)();

#if defined(ENV64BIT)
    typedef void(__fastcall* CmdInsFootnoteAction)(PVOID a1, struct _SEL* a2, long a3, USHORT a4, PVOID a5);
#else
    typedef int(__fastcall* CmdInsFootnoteAction)(unsigned int a1, int a2, int a3, int a4, int a5);
#endif
typedef void(__cdecl* InsertFootnoteCallback)();


struct HookData {
	const char* Signature;
	DWORD64 Offset;
};

struct AlernativeHookData {
	const char* Signature;
	DWORD64 Offset;
	DWORD64 SizeOfAssemblyInstruction;
};

struct HooksData {
	HookData CmdPasteCf;
	std::vector<AlernativeHookData> CmdPasteCfAlternatives;

	HookData CmdInsertBreakXchCore;
	std::vector<AlernativeHookData> CmdInsertBreakXchCoreAlternatives;

	HookData InsertXch;
	std::vector<AlernativeHookData> InsertXchAlternatives;

	HookData ItapDocCp;
	std::vector<AlernativeHookData> ItapDocCpAlternatives;

	HookData FUndoCore;
	std::vector<AlernativeHookData> FUndoCoreAlternatives;

	HookData DoWordReplace;
	std::vector<AlernativeHookData> DoWordReplaceAlternatives;

	HookData  InsertPictureFiles;
	std::vector<AlernativeHookData>  InsertPictureFilesAlternatives;

	HookData CmdInsFootnoteAction;
	std::vector<AlernativeHookData> CmdInsFootnoteActionAlternatives;

	HookData SaveText;
	std::vector<AlernativeHookData>  SaveTextAlternatives;

	HookData cpCacheTap;
};
class Hooks {
	public:
		Hooks(Data* data);
		bool HookOnPasteText(PasteHookCallback Hook);
		bool HookOnTextInput(InsertHookCallback Hook);
		bool HookSave(SaveCallback Hook);
		bool HookUndo(UndoCallback Hook);
		bool HookDoWordReplace(DoWordReplaceCallback Hook);
		bool HookOnImageInput(InsertPictureFilesCallback Hook);
		bool HookOnInsertBreak(InsertBreakCallback Hook);
		bool HookOnInsertFootnote(InsertFootnoteCallback Hook);
	private:
		HooksData hooksData;
		PVOID GetAlternative(std::vector<AlernativeHookData> alternatives);
		HooksData Get32HookData();
		HooksData Get64HookData();
		char* modBase;
		bool HookCp();
};

