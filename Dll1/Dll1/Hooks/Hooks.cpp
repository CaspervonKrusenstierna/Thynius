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

SaveText pSaveText = nullptr;
PVOID pSaveTextTarget;
SaveCallback funcToRunOnSave;
unsigned long __fastcall detourSaveText(void const* p1, void const* p2, int p3, wchar_t const* p4, struct _GUID const* p5, int p6, struct _GUID const* p7, int p8, int p9, int p10, int p11, int p12, int p13, int p14, unsigned int p15, int p16, int p17, int p18, unsigned int p19, unsigned int p20) {
	funcToRunOnSave();
	return pSaveText(p1, p2, p3, p4, p5, p6, p7 ,p8 ,p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);

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

	return true;
}

bool Hooks::HookSave(SaveCallback Hook) {
	funcToRunOnSave = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, "48 8D 05 ? ? ? ? 4C 8B AD ? ? ? ? 49 8B D9 44 8B A5 ? ? ? ?");
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pSaveTextTarget = PVOID((DWORD64)PatternAddy - 0x20);

	if (MH_CreateHook(pSaveTextTarget, &detourSaveText, (LPVOID*)&pSaveText) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	return true;
}