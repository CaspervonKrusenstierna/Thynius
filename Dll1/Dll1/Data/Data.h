#pragma once
#include <Windows.h>
#include "../MemUtils/MemUtils.h"
#include <fstream>
#include <iostream>
#include <string>

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

//offsets from wwlib.dll
enum Offsets {
	CHARCOUNT = 0x29846A0
};

enum DODType {
	MAINDOC = 1,
	HEADER = 1024,
	FOOTNOTE = 2048,
};

struct Selection {
	UINT32 SelectionStart;
	UINT32 SelectionEnd;
};

struct X32SelectionOption {
	const char* Sig;
	DWORD64 Offset;
	DWORD64 InstructionSize;
};

struct DefaultOption {
	const char* Sig;
	DWORD64 Offset;
};

struct DataData {
	//X64: FLockForEditDRM
	//X32: GetSelCurChpCore
	const char* SelectionPattern;
	DWORD64 SelectionPatternOffset;
	DWORD64 SelectionPatternInstructionSize;

	std::vector<DefaultOption> CpRODOffsetOptions;
	const char* CpRODOffsetPattern;
	DWORD64 CpRODOffsetOffset;

	std::vector<DefaultOption> DODTypeOffsetOptions;
	const char* DODTypeOffsetPattern;
	DWORD64 DODTypeOffsetOffset;

	const char* PageCountOffsetPattern;
	DWORD64 PageCountOffsetPatternOffset;
	DWORD64 PageCountOffsetPatternInstructionSize;
};

class Data {
public:
	Data();
	Selection GetSelection();
	UINT64 GetMainDocCp();
	UINT64 GetFootnoteCp();
	UINT64 GetHeaderCp();
	UINT32 GetPageCount();

	void AddDOD(PVOID DOD);
	PVOID footnoteDOD;
	PVOID mainDOD;
	PVOID HeaderDOD;
	PVOID FooterDOD;
private:
	std::vector<PVOID> unknownDODS = std::vector<PVOID>();
	PVOID selAddy;
	INT32 CpRODOffset;
	INT32 DODTypeOffset;
	PVOID PageCountAddy;
	char* modBase;
	PVOID UseX32SelectionOption();

	INT32 UseDefaultOption(std::vector<DefaultOption> Options);
	PVOID UseX32SelectionOption(std::vector<X32SelectionOption> Options);

	void RunX64Data();
	void RunX32Data();

	DataData Get64DataData();
	DataData Get32DataData();
	DataData dataData;
};