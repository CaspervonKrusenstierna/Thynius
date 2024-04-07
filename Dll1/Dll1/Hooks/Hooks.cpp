#include "Hooks.h"

struct DOD* dod = 0;
std::wofstream Output3;
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
	funcToRunOnSave(p5->Data1);
	return pSaveText(p1, p2, p3, p4, p5, p6, p7 ,p8 ,p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);
}

FUndoCore pUndo = nullptr;
PVOID pUndoTarget;
UndoCallback funcToRunOnUndo;
void __fastcall detourUndo(PVOID isUndo, struct DOD* a2, int a3, int a4) {
	if (funcToRunOnUndo(reinterpret_cast<UINT64>(isUndo))) {
		return pUndo(isUndo, a2, a3, a4);
	}
}

CpFirstDocCp pCp;
PVOID pCpTarget;

int count = 0;
__int64 __fastcall detourCpFirstDocCp(struct DOD* a1, int a2) {
	if (dod == 0) {
		dod = a1;
	}
	return pCp(a1, a2);
}

Hooks::Hooks() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	modBase = (char*)ModInfo.lpBaseOfDll;
	MH_Initialize();
	HookCp();
}

bool Hooks::HookOnPasteText(PasteHookCallback Hook) {
	funcToRunOnPaste = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, "B8 ? ? ? ? 89 9D ? ? ? ? 66 39 85 ? ? ? ? 75 07");

	if (PatternAddy == NULL) {
		return FALSE;
	}

	pCmdPasteCfTarget = (PVOID)((DWORD64)PatternAddy - 0x1E7);
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

bool Hooks::HookCp() {
	PVOID PatternAddy = PatternScan(this->modBase, "48 83 EC 28 8B 81 ? ? ? ? 3D ? ? ? ? 74 32");
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pCpTarget = PatternAddy;

	if (MH_CreateHook(pCpTarget, &detourCpFirstDocCp, (LPVOID*)&pCp) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	return true;
}

bool Hooks::HookUndo(UndoCallback Hook) {
	funcToRunOnUndo = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, "48 89 85 ? ? ? ? 41 8B F1 44 89 4D CC 44 89 45 C8 48 8B DA");
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pUndoTarget = (PVOID)((DWORD64)PatternAddy - 0x2C);;

	if (MH_CreateHook(pUndoTarget, &detourUndo, (LPVOID*)&pUndo) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}

	return true;
}




INT64 GetCpCount(UINT32 CpOffset) {
	return *reinterpret_cast<INT64*>(reinterpret_cast<DWORD64>(dod) + CpOffset);
}

