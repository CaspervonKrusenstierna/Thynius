#include "Hooks.h"

PVOID dod = 0;
std::vector<PVOID> knownDODS = std::vector<PVOID>();
Data* _data;
int cachedGUID = NULL;
std::wofstream DebugOutputHooks("C:\\ProgramData\\Thynius\\DebugHooks");

InsertPictureFiles pInsertPictureFiles = nullptr;
PVOID pInsertPictureFilesTarget;
InsertPictureFilesCallback funcToRunOnImageInsert;

#if defined(ENV64BIT)
void __fastcall detourInsertPictureFiles(PVOID a1, PVOID* a2, PVOID a3, PVOID* a4, PVOID* a5, PVOID* a6) {
	funcToRunOnImageInsert();
	return pInsertPictureFiles(a1, a2, a3, a4, a5, a6);
}
#else
void __fastcall detourInsertPictureFiles(PVOID a1, PVOID a2, PVOID a3, PVOID a4, PVOID a5, PVOID a6, PVOID a7) { // todo: fix image insert on x32. rn this only disables
	//funcToRunOnImageInsert();
	return pInsertPictureFiles(a1, a2, a3, a4, a5, a6, a7);
}
#endif

CmdPasteCf pCmdPasteCf = nullptr;
PVOID pCmdPasteCfTarget;
PasteHookCallback funcToRunOnPaste;

#if defined(ENV64BIT)
    enum CMD __fastcall detourCmdPasteCf(struct IDataObject* a1, struct _SEL* a2, unsigned short a3, struct CA* a4, enum TBPK a5, unsigned short a6, int a7, unsigned short* a8, int* a9, int a10) {
	std::wstring clipboardText = GetClipboardText();
	funcToRunOnPaste(clipboardText);
	return pCmdPasteCf(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
	}
#else
	int* __fastcall detourCmdPasteCf(char* a1, int a2, __int16 a3, char* a4, int a5, __int16 a6, int a7, int a8, int a9, int a10) {
		std::wstring clipboardText = GetClipboardText();
		funcToRunOnPaste(clipboardText);
		return pCmdPasteCf(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
	}
#endif

InsertXch pInsertTextAtSelection = nullptr;
PVOID pInsertTextAtSelectionTarget;
InsertHookCallback funcToRunOnInsert;

#if defined(ENV64BIT)
void __fastcall detourInsertTextAtSelection(PVOID p1, wchar_t Input) {
	funcToRunOnInsert(Input);
	return pInsertTextAtSelection(p1, Input);
}
#else
int __fastcall detourInsertTextAtSelection(PVOID p1, PVOID p2, wchar_t Input) {
	funcToRunOnInsert(Input);
	return pInsertTextAtSelection(p1, Input);
}
#endif

EventWriteEvtWordSaveStart pSaveText = nullptr;
PVOID pSaveTextTarget;
SaveCallback funcToRunOnSave;
#if defined(ENV64BIT)
unsigned long __fastcall detourEventWriteEvtWordSaveStart(void const* p1, void const* p2, int p3, wchar_t const* p4, struct _GUID const* p5, int p6, struct _GUID const* p7, int p8, int p9, int p10, int p11, int p12, int p13, int p14, unsigned int p15, int p16, int p17, int p18, unsigned int p19, unsigned int p20) {
	if (cachedGUID == NULL || p5->Data1 == cachedGUID) {
		cachedGUID = p5->Data1;
		funcToRunOnSave(p5->Data1);
	}
	return pSaveText(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);
}
#else
PVOID __fastcall detourEventWriteEvtWordSaveStart(PVOID a1, PVOID a2, int a3, const wchar_t* a4, const struct _GUID* a5, int a6, const struct _GUID* a7, int a8, int a9, int a10, int a11, int a12, int a13, int a14, unsigned int a15, int a16, int a17, int a18, unsigned int a19, unsigned int a20) {
	if (cachedGUID == NULL || a7->Data1 == cachedGUID) {
		cachedGUID = a7->Data1;
		funcToRunOnSave(a7->Data1);
	}
	return pSaveText(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20);
}
#endif

FUndoCore pUndo = nullptr;
PVOID pUndoTarget;
UndoCallback funcToRunOnUndo;
#if defined(ENV64BIT)
void __fastcall detourUndo(PVOID isUndo, struct DOD* a2, int a3, int a4) {
	if (funcToRunOnUndo(reinterpret_cast<UINT64>(isUndo))) {
		return pUndo(isUndo, a2, a3, a4);
	}
}
#else
bool __fastcall detourUndo(bool isUndo, PVOID a2, int a3, int a4) {
	if (funcToRunOnUndo((UINT64)isUndo)) {
		return pUndo(isUndo, a2, a3, a4);
	}
}
#endif

CmdInsFootnoteAction pCmdInsFootnoteAction = nullptr;
PVOID pCmdInsFootnoteActionTarget;
InsertFootnoteCallback funcToRunOnInsertFootnote;
#if defined(ENV64BIT)
void __fastcall detourCmdInsFootnoteAction(PVOID a1, _SEL* a2, long a3, USHORT a4, PVOID a5) {
	funcToRunOnInsertFootnote();
	pCmdInsFootnoteAction(a1, a2, a3, a4, a5);
}
#else
void __fastcall detourCmdInsFootnoteAction(unsigned int a1, int a2, int a3, int a4, int a5) {
	funcToRunOnInsertFootnote();
	pCmdInsFootnoteAction(a1, a2, a3, a4, a5);
}
#endif


DoWordReplace pDoWordReplace = nullptr;
PVOID pDoWordReplaceTarget;
DoWordReplaceCallback funcToRunOnDoWordReplace;
#if defined(ENV64BIT)
__int64 __fastcall detourDoWordReplace(const struct WWD* a1, unsigned int* a2, const struct DOD** dod, wchar_t* replacement, DWORD* a5, int a6, char a7, unsigned __int8 a8, __int64 a9) {
	UINT32 replacementStart = *a2;
	UINT32 replacementEnd = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(a2) + 0x4);
	funcToRunOnDoWordReplace(std::wstring(replacement), replacementStart, replacementEnd);
	return pDoWordReplace(a1, a2, dod, replacement, a5, a6, a7, a8, a9);
}
#else
int __fastcall detourDoWordReplace(int a1, DWORD* a2, int* dod, wchar_t* replacement, DWORD* a5, int a6, char a7, int a8, int a9, int a10) {
	UINT32 replacementStart = *a2;
	UINT32 replacementEnd = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(a2) + 0x4);
	funcToRunOnDoWordReplace(std::wstring(replacement), replacementStart, replacementEnd);
	return pDoWordReplace(a1, a2, dod, replacement, a5, a6, a7, a8, a9, a10);
}
#endif


CmdInsertBreakXchCore pCmdInsertBreakXchCore = nullptr;
PVOID pCmdInsertBreakXchCoreTarget;
InsertBreakCallback funcToRunOnInsertBreak;
#if defined(ENV64BIT)
void  detourCmdInsertBreakXchCore(__int64 a1, wchar_t a2, unsigned __int16 a3, int a4, __int64 a5, char a6) {
	//funcToRunOnInsertBreak();
	return pCmdInsertBreakXchCore(a1, a2, a3, a4, a5, a6);
}
#else
int __fastcall detourCmdInsertBreakXchCore(int a1, __int16 a2, unsigned __int16 a3, int a4, int a5, char a6) {
	//funcToRunOnInsertBreak();
	return pCmdInsertBreakXchCore(a1, a2, a3, a4, a5, a6);
}
#endif

ItapDocCp pCp;
PVOID pCpTarget;

#if defined(ENV64BIT)
__int64 __fastcall detourItapDocCp(PVOID a1, int a2) {
	bool isKnown = false;
	for (int i = 0; knownDODS.size() > i; i++) {
		if (knownDODS[i] == a1) {
			isKnown = true;
		}
	}
	if (!isKnown) {
		_data->AddDOD(a1);
		knownDODS.push_back(a1);
	}
	if (dod == 0) {
		dod = a1;
	}
	return pCp(a1, a2);
}
#else
int __fastcall detourItapDocCp(PVOID a1, int a2) {
	bool isKnown = false;
	for (int i = 0; knownDODS.size() > i; i++) {
		if (knownDODS[i] == reinterpret_cast<PVOID>(a1)) {
			isKnown = true;
		}
	}
	if (!isKnown) {
		_data->AddDOD(a1);
		knownDODS.push_back(a1);
	}
	if (dod == 0) {
		dod = a1;
	}
	return pCp(a1, a2);
}
#endif

PVOID Hooks::GetAlternative(std::vector<AlernativeHookData> alternatives) {
	for (int i = 0; alternatives.size() > i; i++) {
		AlernativeHookData currAlternative = alternatives[i];
		PVOID CurrPattern = PatternScan(this->modBase, currAlternative.Signature);
		DebugOutputHooks << "CurrPattern: " << CurrPattern << std::endl;
		if (CurrPattern) {
			PVOID toReturn = reinterpret_cast<PVOID>(*reinterpret_cast<uint32_t*>(reinterpret_cast<DWORD64>(CurrPattern) + currAlternative.Offset) + reinterpret_cast<DWORD64>(CurrPattern) + currAlternative.Offset + currAlternative.SizeOfAssemblyInstruction);
			DebugOutputHooks << "ToReturn: " << toReturn << std::endl;
			return toReturn;
		}
	}
	return NULL;
}

bool Hooks::HookOnPasteText(PasteHookCallback Hook) {
	funcToRunOnPaste = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.CmdPasteCf.Signature);

	if (PatternAddy == NULL) {
		pCmdPasteCfTarget = GetAlternative(this->hooksData.CmdPasteCfAlternatives);
	}
	else {
		pCmdPasteCfTarget = (PVOID)((DWORD)PatternAddy + this->hooksData.CmdPasteCf.Offset);
	}
	if (pCmdPasteCfTarget == NULL) {
		return false;
	}

	DebugOutputHooks << "PasteTextAddy: " << pCmdPasteCfTarget << std::endl;

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
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.InsertXch.Signature);
	if (PatternAddy == NULL) {
		pInsertTextAtSelectionTarget = GetAlternative(this->hooksData.InsertXchAlternatives);
	}
	else {
		pInsertTextAtSelectionTarget = PVOID((DWORD64)PatternAddy + this->hooksData.InsertXch.Offset);
	}
	if (pInsertTextAtSelectionTarget == NULL) {
		return false;
	}

	DebugOutputHooks << "TextInsertAddy: " << pInsertTextAtSelectionTarget << std::endl;
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
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.SaveText.Signature);
	if (PatternAddy == NULL) {
		return FALSE;
	}

	pSaveTextTarget = PVOID((DWORD64)PatternAddy + this->hooksData.SaveText.Offset);

	if (MH_CreateHook(pSaveTextTarget, &detourEventWriteEvtWordSaveStart, (LPVOID*)&pSaveText) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	return true;
}

bool Hooks::HookCp() {
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.ItapDocCp.Signature);
	if (PatternAddy == NULL) {
		pCpTarget = GetAlternative(this->hooksData.ItapDocCpAlternatives);
	}
	else {
		pCpTarget = PVOID((DWORD64)PatternAddy + this->hooksData.ItapDocCp.Offset);
	}
	if (pCpTarget == NULL) {
		return false;
	}

	if (MH_CreateHook(pCpTarget, &detourItapDocCp, (LPVOID*)&pCp) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}
	return true;
}

bool Hooks::HookUndo(UndoCallback Hook) {
	funcToRunOnUndo = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.FUndoCore.Signature);

	if (PatternAddy == NULL) {
		pUndoTarget = GetAlternative(this->hooksData.FUndoCoreAlternatives);
	}
	else {
		pUndoTarget = (PVOID)((DWORD64)PatternAddy + this->hooksData.FUndoCore.Offset);
	}

	if (pUndoTarget == NULL) {
		return false;
	}

	if (MH_CreateHook(pUndoTarget, &detourUndo, (LPVOID*)&pUndo) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}

	return true;
}

bool Hooks::HookDoWordReplace(DoWordReplaceCallback Hook) {
	funcToRunOnDoWordReplace = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.DoWordReplace.Signature);
	if (PatternAddy == NULL) {
		return FALSE;
	}

    pDoWordReplaceTarget = (PVOID)((DWORD64)PatternAddy + this->hooksData.DoWordReplace.Offset);

	if (MH_CreateHook(pDoWordReplaceTarget, &detourDoWordReplace, (LPVOID*)&pDoWordReplace) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}

	return true;
}

bool Hooks::HookOnImageInput(InsertPictureFilesCallback Hook) {
	funcToRunOnImageInsert = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.InsertPictureFiles.Signature);
	if (PatternAddy == NULL) {
		return FALSE;
	}
	pInsertPictureFilesTarget = (PVOID)((DWORD64)PatternAddy + this->hooksData.InsertPictureFiles.Offset);


	if (MH_CreateHook(pInsertPictureFilesTarget, &detourInsertPictureFiles, (LPVOID*)&pInsertPictureFiles) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}

	return true;
}

bool Hooks::HookOnInsertFootnote(InsertFootnoteCallback Hook) {
	funcToRunOnInsertFootnote = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.CmdInsFootnoteAction.Signature);
	if (PatternAddy == NULL) {
		pCmdInsFootnoteActionTarget = GetAlternative(this->hooksData.CmdInsFootnoteActionAlternatives);
	}
	else {
		pCmdInsFootnoteActionTarget = (PVOID)((DWORD64)PatternAddy + this->hooksData.CmdInsFootnoteAction.Offset);
	}
	if (pCmdInsFootnoteActionTarget == NULL) {
		return false;
	}


	if (MH_CreateHook(pCmdInsFootnoteActionTarget, &detourCmdInsFootnoteAction, (LPVOID*)&pCmdInsFootnoteAction) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}

	return true;
}

bool Hooks::HookOnInsertBreak(InsertBreakCallback Hook) {
	funcToRunOnInsertBreak = Hook;
	PVOID PatternAddy = PatternScan(this->modBase, this->hooksData.CmdInsertBreakXchCore.Signature);
	if (PatternAddy == NULL) {
		pCmdInsertBreakXchCoreTarget = GetAlternative(this->hooksData.CmdInsertBreakXchCoreAlternatives);
	}
	else {
		pCmdInsertBreakXchCoreTarget = (PVOID)((DWORD64)PatternAddy + this->hooksData.CmdInsertBreakXchCore.Offset);
	}
	if (pCmdInsertBreakXchCoreTarget == NULL) {
		return false;
	}


	if (MH_CreateHook(pCmdInsertBreakXchCoreTarget, &detourCmdInsertBreakXchCore, (LPVOID*)&pCmdInsertBreakXchCore) != MH_OK) {
		return FALSE;
	}

	if (MH_EnableHook(MH_ALL_HOOKS) != MH_OK) {
		return FALSE;
	}

	return true;
}

PVOID GetDOD() {
	return dod;
}

HooksData Hooks::Get32HookData() {
	DebugOutputHooks << "Running x32 version!" << std::endl;

	HooksData toReturn;
	AlernativeHookData currAlternative;

	toReturn.CmdInsertBreakXchCore.Signature = "33 DB 56 57 89 85 ? ? ? ? 8D BD ? ? ? ?";
	toReturn.CmdInsertBreakXchCore.Offset = - 0x1E;

	currAlternative.Signature = "68 ? ? ? ? 6A 0E 58 8B D0";
	currAlternative.Offset = 11;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.CmdInsertBreakXchCoreAlternatives.push_back(currAlternative);


	toReturn.CmdInsFootnoteAction.Signature = "83 64 24 ? ? 8B D9 83 64 24 ? ? 56 57 8B FA 89 5C 24 0C";
	toReturn.CmdInsFootnoteAction.Offset = -0x1B;

	currAlternative.Signature = "8B CE 50 E8 ? ? ? ? 8B F8 8B C7";
	currAlternative.Offset = 4;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.CmdInsFootnoteActionAlternatives.push_back(currAlternative);

	toReturn.InsertXch.Signature = "55 8B EC 53 56 8B F1 57 8D 5E 04 8B 03";
	toReturn.InsertXch.Offset = 0x0;


	toReturn.FUndoCore.Signature = "89 9D ? ? ? ? 89 8D ? ? ? ? 83 A5 ? ? ? ? ?";
	toReturn.FUndoCore.Offset = -0x34;

	currAlternative.Signature = "52 52 8B 53 0C B1 01 8B 52 0C";
	currAlternative.Offset = 11;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.FUndoCoreAlternatives.push_back(currAlternative);

	currAlternative.Signature = "8B 53 60 B1 01 E8 ? ? ? ?";
	currAlternative.Offset = 6;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.FUndoCoreAlternatives.push_back(currAlternative);



	toReturn.CmdPasteCf.Signature = "89 85 ? ? ? ? 8B 43 1C 89 85 ? ? ? ? 8B 43 0C 89 85 ? ? ? ?";
	toReturn.CmdPasteCf.Offset = -0x55;

	currAlternative.Signature = "FF 74 24 30 FF 75 24 FF 75 20 FF 75 10 FF 74 24 44 FF 75 08";
	currAlternative.Offset = 21;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.CmdPasteCfAlternatives.push_back(currAlternative);

	currAlternative.Signature = "0F 95 C1 41 6A 01 51 B1 01";
	currAlternative.Offset = 10;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.CmdPasteCfAlternatives.push_back(currAlternative);


	toReturn.SaveText.Signature = "55 8B EC 81 EC ? ? ? ? 53 56 57 FF 75 4C";
	toReturn.SaveText.Offset = 0x0;


	toReturn.DoWordReplace.Signature = "56 57 89 85 ? ? ? ? 8B 45 10";
	toReturn.DoWordReplace.Offset = -0x1A;


	toReturn.InsertPictureFiles.Signature = "89 45 E0 83 65 FC 00 83 65 F0 00 C6 45 FC 01 FF 15 ? ? ? ?";
	toReturn.InsertPictureFiles.Offset = -0x16;


	toReturn.ItapDocCp.Signature = "55 8B EC 83 E4 F8 83 EC 3C A1 ? ? ? ? 33 C4 89 44 24 38 53 56 57 FF 35 ? ? ? ?";
	toReturn.ItapDocCp.Offset = 0x0;

	currAlternative.Signature = "8B 4B 04 39 0B 7C 35 8B 13 8B C8 E8 ? ? ? ?";
	currAlternative.Offset = 12;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.ItapDocCpAlternatives.push_back(currAlternative);

	toReturn.cpCacheTap.Signature = "55 8B EC 83 EC 10 53 56 57 FF 35 ? ? ? ? 8B 3D ? ? ? ? 8B DA 8B F1 89 5D FC";
	toReturn.cpCacheTap.Offset = 0;

	return toReturn;
}

HooksData Hooks::Get64HookData() {
	AlernativeHookData currAlternative;
	DebugOutputHooks << "Running x64 version!" << std::endl;
	HooksData toReturn;
	toReturn.CmdInsertBreakXchCore.Signature = "48 8D 8D ? ? ? ? 33 D2 44 89 65 98 41 B8 ? ? ? ?";
	toReturn.CmdInsertBreakXchCore.Offset = -0x4B;

	toReturn.CmdInsFootnoteAction.Signature = "48 89 84 24 ? ? ? ? 48 8B 42 0C 48 8B E9";
	toReturn.CmdInsFootnoteAction.Offset = -0x1C;

	toReturn.InsertXch.Signature = "FF 87 ? ? ? ? 89 B7 ? ? ? ? E8 ? ? ? ?";
	toReturn.InsertXch.Offset = -0x32;

	toReturn.FUndoCore.Signature = "48 89 85 ? ? ? ? 41 8B F1 44 89 4D CC 44 89 45 C8 48 8B DA";
	toReturn.FUndoCore.Offset = -0x2C;

	toReturn.CmdPasteCf.Signature = "B8 ? ? ? ? 89 9D ? ? ? ? 66 39 85 ? ? ? ? 75 07";
	toReturn.CmdPasteCf.Offset = -0x1E7;
	currAlternative.Signature = "49 8B CF E8 ? ? ? ? 8B D8 44 21 65 AC";
	currAlternative.Offset = 4;
	currAlternative.SizeOfAssemblyInstruction = 4;
	toReturn.CmdPasteCfAlternatives.push_back(currAlternative);

	toReturn.SaveText.Signature = "48 8D 05 ? ? ? ? 4C 8B AD ? ? ? ? 49 8B D9 44 8B A5 ? ? ? ?";
	toReturn.SaveText.Offset = -0x20;

	toReturn.DoWordReplace.Signature = "48 89 4C 24 ? 33 D2 4C 89 7C 24 ?";
	toReturn.DoWordReplace.Offset = -0x3A;

	toReturn.InsertPictureFiles.Signature = "48 81 EC ? ? ? ? 4D 8B F9 48 8B F1 48 8B 49 30 E8 ? ? ? ?";
	toReturn.InsertPictureFiles.Offset = -0x1B;

	toReturn.ItapDocCp.Signature = "53 56 57 48 8B EC 48 83 EC 78 48 8B 05 ? ? ? ?";
	toReturn.ItapDocCp.Offset = -0x2;

	return toReturn;
}

Hooks::Hooks(Data* data) {
	MH_Initialize();
	_data = data;
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	modBase = (char*)ModInfo.lpBaseOfDll;

	#if defined(ENV64BIT)
	    this->hooksData = Get64HookData();
	#elif defined (ENV32BIT)
	    this->hooksData = Get32HookData();
	#else
	#error "Must define either ENV32BIT or ENV64BIT".
	#endif

	if (!HookCp()) {
		DebugOutputHooks << "Fatal error: failed to hook cp." << std::endl;
	}

}