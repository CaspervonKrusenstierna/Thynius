#pragma once
#include <iostream>
#include <Windows.h>
#include "../SystemUtils/SystemUtils.h"
#include "../MemUtils/MemUtils.h"
#pragma comment(lib, "libMinHook.x64.lib")
#include <fstream>
#include <MinHook.h>
#include <string>


typedef enum CMD(__cdecl* CmdPasteCf)(struct IDataObject*, struct _SEL*, unsigned short, struct CA*, enum TBPK, unsigned short, int, unsigned short*, int*, int);
typedef void(__cdecl* PasteHookCallback)(std::wstring ClipboardContent);

typedef void(__thiscall* InsertTextAtSelection)(PVOID, wchar_t);
typedef void(__cdecl* InsertHookCallback)(wchar_t);

typedef unsigned long(__cdecl* SaveText)(void const*, void const*, int, wchar_t const*, struct _GUID const*, int, struct _GUID const*, int, int, int, int, int, int, int, unsigned int, int, int, int, unsigned int, unsigned int);
typedef void(__cdecl* SaveCallback)(unsigned long GUID);

typedef __int64(__fastcall* CpFirstDocCp)(struct DOD* a1, int a2);
INT64 GetCpCount(UINT32 CpOffset);

typedef void(__fastcall* FUndoCore)(PVOID isUndo, struct DOD* a2, int a3, int a4);
typedef bool(__cdecl* UndoCallback)(UINT64 isUndo);

typedef __int64(__fastcall* DoWordReplace)(const struct WWD* a1, unsigned int* a2, const struct DOD** dod, wchar_t* replacement, DWORD* a5, int a6, char a7, unsigned __int8 a8, __int64 a9);
typedef void(__cdecl* DoWordReplaceCallback)(std::wstring replacement, UINT32 replacementStart, UINT32 replacementEnd);

class Hooks {
	public:
		Hooks();
		bool HookOnPasteText(PasteHookCallback Hook);
		bool HookOnTextInput(InsertHookCallback Hook);
		bool HookSave(SaveCallback Hook);
		bool HookUndo(UndoCallback Hook);
		bool HookDoWordReplace(DoWordReplaceCallback Hook);

	private:
		char* modBase;
		bool HookCp();
};

