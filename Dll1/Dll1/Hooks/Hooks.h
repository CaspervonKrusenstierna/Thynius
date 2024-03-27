#pragma once
#include <iostream>
#include <Windows.h>
#include "../SystemUtils/SystemUtils.h"
#include "../MemUtils/MemUtils.h"
#pragma comment(lib, "libMinHook.x64.lib")
#include <fstream>
#include <MinHook.h>

typedef enum CMD(__cdecl* CmdPasteCf)(struct IDataObject*, struct _SEL*, unsigned short, struct CA*, enum TBPK, unsigned short, int, unsigned short*, int*, int);
typedef void(__cdecl* PasteHookCallback)(std::wstring ClipboardContent);

typedef void(__thiscall* InsertTextAtSelection)(PVOID, wchar_t);
typedef void(__cdecl* InsertHookCallback)(wchar_t);

typedef unsigned long(__cdecl* SaveText)(void const*, void const*, int, wchar_t const*, struct _GUID const*, int, struct _GUID const*, int, int, int, int, int, int, int, unsigned int, int, int, int, unsigned int, unsigned int);
typedef void(__cdecl* SaveCallback)();

class Hooks {
	private:
		char* modBase;

	public:
		Hooks();
		bool HookOnPasteText(PasteHookCallback Hook);
		bool HookOnTextInput(InsertHookCallback Hook);
		bool HookSave(SaveCallback Hook);
};

