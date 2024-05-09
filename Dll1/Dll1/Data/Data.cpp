#include "Data.h"
std::wofstream DebugOutputTest("C:\\ProgramData\\Thynius\\DebugData");

UINT64 Data::GetMainDocCp() {
	if (!this->mainDOD) {
		for (int i = 0; unknownDODS.size() > i; i++) {
			PVOID CurrDOD = unknownDODS[i];
			try {
				UINT32 DODType = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(CurrDOD) + this->DODTypeOffset);
				if (DODType == DODType::MAINDOC) {
					this->mainDOD = CurrDOD;
					return *reinterpret_cast<UINT64*>(reinterpret_cast<DWORD64>(CurrDOD) + this->CpRODOffset);
				}
			}
			catch (int ex) {
				unknownDODS.erase(unknownDODS.begin() + i);
			}
		}
		return 0;
	}
	return *reinterpret_cast<UINT64*>(reinterpret_cast<DWORD64>(this->mainDOD) + this->CpRODOffset);
}

UINT32 Data::GetPageCount() {
	return *reinterpret_cast<UINT32*>(this->PageCountAddy);
}

UINT64 Data::GetFootnoteCp() {
	if (!this->footnoteDOD) {
		for (int i = 0; unknownDODS.size() > i; i++) {
			PVOID CurrDOD = unknownDODS[i];
			try {
				UINT32 DODType = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(CurrDOD) + this->DODTypeOffset);
				if (DODType == DODType::FOOTNOTE) {
					this->footnoteDOD = CurrDOD;
					return *reinterpret_cast<UINT64*>(reinterpret_cast<DWORD64>(CurrDOD) + this->CpRODOffset);
				}
			}
			catch (int ex) {
				unknownDODS.erase(unknownDODS.begin() + i);
			}
		}
		return 0;
	}
	return *reinterpret_cast<UINT64*>(reinterpret_cast<DWORD64>(this->footnoteDOD) + this->CpRODOffset);
}

UINT64 Data::GetHeaderCp() {
	if (!this->HeaderDOD) {
		for (int i = 0; unknownDODS.size() > i; i++) {
			PVOID CurrDOD = unknownDODS[i];
			try {
				UINT32 DODType = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(CurrDOD) + this->DODTypeOffset);
				if (DODType == DODType::HEADER) {
					this->HeaderDOD = CurrDOD;
					return *reinterpret_cast<UINT64*>(reinterpret_cast<DWORD64>(CurrDOD) + this->CpRODOffset);
				}
			}
			catch (int x) {
				unknownDODS.erase(unknownDODS.begin() + i); // bad dod 
			}
		}
		return 0;
	}
	return *reinterpret_cast<UINT64*>(reinterpret_cast<DWORD64>(this->HeaderDOD) + this->CpRODOffset);
}


void Data::AddDOD(PVOID DOD) {
	UINT32 DODType = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(DOD) + this->DODTypeOffset);

	switch (DODType) {
		case DODType::MAINDOC:
			if (this->mainDOD) {
				this->unknownDODS.push_back(DOD);
			}
			else {
				this->mainDOD = DOD;
			}
			break;

		case DODType::FOOTNOTE:
			if (this->footnoteDOD) {
				this->unknownDODS.push_back(DOD);
			}
			else {
				this->footnoteDOD = DOD;
			}
			break;

		case DODType::HEADER:
			if (this->HeaderDOD) {
				this->unknownDODS.push_back(DOD);
			}
			else {
				this->HeaderDOD = DOD;
			}
			break;
	}
}

Selection Data::GetSelection() {
	Selection toReturn;
	toReturn.SelectionStart = *reinterpret_cast<UINT32*>(selAddy);
	toReturn.SelectionEnd = *reinterpret_cast<UINT32*>(reinterpret_cast<DWORD64>(selAddy) + 0x4);
	return toReturn;
}

DataData Data::Get64DataData() {
	DataData toReturn;
	toReturn.SelectionPattern = "0f 10 35 ? ? ? ? 66 0f 6f c6";
	toReturn.SelectionPatternOffset = 0x3;
	toReturn.SelectionPatternInstructionSize = 0x7;

	toReturn.CpRODOffsetPattern = "44 39 11 74 ? 45 8b ce 44 8b c7 8b d3 e8 ? ? ? ? ba";
	toReturn.CpRODOffsetOffset = -0x4;

	toReturn.DODTypeOffsetPattern = "40 53 48 83 ec ? 8b 81 ? ? ? ? 48 8b da";
	toReturn.DODTypeOffsetOffset = 0x8;

	return toReturn;
}

DataData Data::Get32DataData(){
	DefaultOption currDefaultOption;
	X32SelectionOption currSelOption;
	DataData toReturn;

	toReturn.SelectionPattern = "A1 ? ? ? ? 0F B6 C0 66 3B C8 0F 85 ? ? ? ?";
	toReturn.SelectionPatternOffset = 0x19;
	toReturn.SelectionPatternInstructionSize = 0x4;

	//FROM CMDINSBOOKMARK
	currDefaultOption.Sig = "8D 8F ? ? ? ? 74 0C 56 FF B5 ? ? ? ? E8 ? ? ? ? BA ? ? ? ?";
	currDefaultOption.Offset = 0x2;
	toReturn.CpRODOffsetOptions.push_back(currDefaultOption);
	currDefaultOption.Sig = "FF 75 E4 E8 ? ? ? ? 81 BE ? ? ? ? ? ? ? ?";
	currDefaultOption.Offset = 0x10;
	toReturn.CpRODOffsetOptions.push_back(currDefaultOption);

	//FROM GETDODKIND
	currDefaultOption.Sig = "8B 81 ? ? ? ? 56 8B F2 BA ? ? ? ?";
	currDefaultOption.Offset = 0x2;
	toReturn.DODTypeOffsetOptions.push_back(currDefaultOption);
	//FROM XszKindFromPdod
	currDefaultOption.Sig = "85 C9 74 36 8B 81 ? ? ? ?";
	currDefaultOption.Offset = 0x6;
	toReturn.DODTypeOffsetOptions.push_back(currDefaultOption);

	return toReturn;
}
INT32 Data::UseDefaultOption(std::vector<DefaultOption> Options) {
	for (int i = 0; Options.size() > i; i++) {
		DefaultOption CurrOption = Options[i];
		PVOID SigAddy = PatternScan(this->modBase, CurrOption.Sig);
		if (SigAddy) {
			return *(reinterpret_cast<INT32*>(reinterpret_cast<DWORD64>(SigAddy) + CurrOption.Offset));
		}
	}
	return 0;
}
PVOID Data::UseX32SelectionOption(std::vector<X32SelectionOption> Options) {
	for (int i = 0; Options.size() > i; i++) {
		X32SelectionOption CurrOption = Options[i];
		PVOID SigAddy = PatternScan(this->modBase, CurrOption.Sig);
		if (SigAddy) {
			return selAddy = reinterpret_cast<uint32_t*>(*reinterpret_cast<uint32_t*>(reinterpret_cast<DWORD64>(SigAddy) + CurrOption.Offset) + reinterpret_cast<DWORD>(SigAddy) + CurrOption.InstructionSize);
		}
	}
	return 0;
}
void Data::RunX32Data() {
	PVOID SelectionPattern = PatternScan(modBase, this->dataData.SelectionPattern);
	if (SelectionPattern == NULL) {
		DebugOutputTest << "Fatal error: failed to get selection selectionpattern" << std::endl;
	}
	selAddy = *reinterpret_cast<uint32_t**>(reinterpret_cast<DWORD>(SelectionPattern) + this->dataData.SelectionPatternOffset);

	CpRODOffset = UseDefaultOption(this->dataData.CpRODOffsetOptions);
	if (!CpRODOffset) {
		DebugOutputTest << "Fatal error: failed to get Offset 1" << std::endl;
	}
	DebugOutputTest << "CpRODOffset: " << CpRODOffset << std::endl;
	DODTypeOffset = UseDefaultOption(this->dataData.DODTypeOffsetOptions);
	if (!DODTypeOffset) {
		DebugOutputTest << "Fatal error: failed to get Offset 2" << std::endl;
	}
	DebugOutputTest << "DODTypeOffset: " << DODTypeOffset << std::endl;
}

void Data::RunX64Data() {
	PVOID SelectionPattern = PatternScan(modBase, this->dataData.SelectionPattern);
	selAddy = reinterpret_cast<uint32_t*>(*reinterpret_cast<uint32_t*>(reinterpret_cast<DWORD64>(SelectionPattern) + this->dataData.SelectionPatternOffset) + reinterpret_cast<DWORD>(SelectionPattern) + this->dataData.SelectionPatternInstructionSize);

	PVOID CpRODOffsetPattern = PatternScan(modBase, this->dataData.CpRODOffsetPattern);
	CpRODOffset = *(reinterpret_cast<INT32*>(reinterpret_cast<DWORD64>(CpRODOffsetPattern) + this->dataData.CpRODOffsetOffset));

	PVOID DODTypeOffsetPattern = PatternScan(modBase, this->dataData.DODTypeOffsetPattern);
	DODTypeOffset = *(reinterpret_cast<INT32*>(reinterpret_cast<DWORD64>(DODTypeOffsetPattern) + this->dataData.DODTypeOffsetOffset));
}
Data::Data() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	this->modBase = (char*)ModInfo.lpBaseOfDll;

	#if defined(ENV64BIT)
		this->dataData = Get64DataData();
		RunX64Data();
	#elif defined (ENV32BIT)
		this->dataData = Get32DataData();
		RunX32Data();
	#else
	   #error "Must define either ENV32BIT or ENV64BIT".
	#endif

}
