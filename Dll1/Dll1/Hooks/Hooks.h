#include <iostream>
#include <Windows.h>
#include "../SystemUtils/SystemUtils.h"
#include "../MemUtils/MemUtils.h"
#pragma comment(lib, "libMinHook.x64.lib")
#include <fstream>
#include <MinHook.h>

typedef enum CMD(__cdecl* CmdPasteCf)(struct IDataObject*, struct _SEL*, unsigned short, struct CA*, enum TBPK, unsigned short, int, unsigned short*, int*, int);
typedef void(__cdecl* PasteHookCallback)(std::string ClipboardContent);

typedef void(__thiscall* InsertTextAtSelection)(PVOID, wchar_t);
typedef void(__cdecl* InsertHookCallback)(wchar_t);



class Hooks {
	private:
		char* modBase;

	public:
		Hooks();
		bool HookOnPasteText(PasteHookCallback Hook);
		bool HookOnTextInput(InsertHookCallback Hook);

};

