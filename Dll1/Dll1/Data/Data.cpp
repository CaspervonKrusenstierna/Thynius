#include "Data.h"

// ptr points to a unicode string structured something like this: "5 words"
UINT32 WordPtrToCount(PVOID ptr) {
	const wchar_t* Delimiter = L" ";
	wchar_t* buffer;
	std::wstring CountStr = wcstok((wchar_t*)ReadWString(ptr).c_str(), Delimiter, &buffer);

	return std::stoi(CountStr);
}
/* this function waits for the wordcountaddy pointerchain to resolve. If it does, the user has opened a text
. when the user opens a text we want to get and save the wordcount and charcount so that we can identify the text
in the server.*/
DWORD WINAPI ResolveWordCountAddy(StartUpIdentifierData & toReturn) {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	char* modBase = (char*)ModInfo.lpBaseOfDll;

	PVOID WordCountAddy;
	UINT32* CharCountAddy = reinterpret_cast<UINT32*>(modBase + Offsets::CHARCOUNT);
	UINT32 temp;
	while (true) {
		try {
			WordCountAddy = reinterpret_cast<PVOID>(FindDMAAddy(reinterpret_cast<DWORD64>(modBase) + 0x2967350, { 0x18, 0x130, 0x178, 0x0 }));
			temp = *CharCountAddy;
			if (temp != 0 && WordCountAddy != 0) {
				// user opened a text
				break;
			}
			throw (temp);
		}
		catch(UINT32 temp){

		}
		Sleep(50);
	}

	toReturn.WordCount = WordPtrToCount(WordCountAddy);
    toReturn.CharCount = *CharCountAddy;

	return 0;
}

Data::Data() {
	MODULEINFO ModInfo = GetModuleInfo(L"wwlib.dll");
	char* modBase = (char*)ModInfo.lpBaseOfDll;
	SelectionStartAddy = reinterpret_cast<UINT32*>(modBase + Offsets::SELECTIONSTART);
	SelectionEndAddy = reinterpret_cast<UINT32*>(modBase + Offsets::SELECTIONEND);
	this->IdentityData.CharCount = 0;
	this->IdentityData.WordCount = 0;
    CloseHandle(CreateThread(nullptr, 0, (LPTHREAD_START_ROUTINE)ResolveWordCountAddy, &this->IdentityData, 0, nullptr));
}

Selection Data::GetSelection() {
	Selection toReturn;
	toReturn.SelectionStart = *SelectionStartAddy;
	toReturn.SelectionEnd = *SelectionEndAddy;
	return toReturn;
}
UINT32 Data::GetCharCount() {
	return IdentityData.CharCount;
}

UINT32 Data::GetWordCount() {
	return IdentityData.WordCount;
}
