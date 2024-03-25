#include "Hooks.h"


CmdPasteCf pCmdPasteCf = nullptr;
PVOID pCmdPasteCfTarget;
PasteHookCallback funcToRunOnPaste;
enum CMD __cdecl detourCmdPasteCf(struct IDataObject* p1, struct _SEL* p2, unsigned short p3, struct CA* p4, enum TBPK p5, unsigned short p6, int p7, unsigned short* p8, int* p9, int p10) {
	std::wstring clipboardText = GetClipboardText();
	funcToRunOnPaste(clipboardText);
	return pCmdPasteCf(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
}

InsertTextAtSelection pInsertTextAtSelection = nullptr;
PVOID pInsertTextAtSelectionTarget;
InsertHookCallback funcToRunOnInsert;

void __fastcall detourInsertTextAtSelection(PVOID p1, wchar_t Input) {
	funcToRunOnInsert(Input);
	return pInsertTextAtSelection(p1, Input);
}

Hooks::Hooks() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	modBase = (char*)ModInfo.lpBaseOfDll;
	MH_Initialize();
}
bool Hooks::HookOnPasteText(PasteHookCallback Hook) {
	funcToRunOnPaste = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, "0F 11 85 ? ? ? ? 0F 11 4D D8 E8 ? ? ? ?");
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pCmdPasteCfTarget = (PVOID)((DWORD64)PatternAddy - 0x97);

	if (MH_CreateHook(pCmdPasteCfTarget, &detourCmdPasteCf, (LPVOID*)&pCmdPasteCf) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}

	return true;
}

bool Hooks::HookOnTextInput(InsertHookCallback Hook){
	funcToRunOnInsert = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, "FF 87 ? ? ? ? 89 B7 ? ? ? ? E8 ? ? ? ?");
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pInsertTextAtSelectionTarget = PVOID((DWORD64)PatternAddy - 0x32);

   if (MH_CreateHook(pInsertTextAtSelectionTarget, &detourInsertTextAtSelection, (LPVOID*)&pInsertTextAtSelection) != MH_OK) {
			return FALSE;
		}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	std::ofstream myfile;
	myfile.open("C:\\file.txt");
	myfile << "InsertCount: " << "Hooked!" << std::endl;
	myfile.close();
	return true;
}