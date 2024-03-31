#pragma once
#include <Windows.h>
#include <iostream>
#include <tlhelp32.h>
#include <Psapi.h>
#include <vector>
#include <array>

MODULEINFO GetModuleInfo(LPCWSTR szModule);
std::uint8_t* PatternScan(void* module, const char* signature);
uintptr_t FindDMAAddy(uintptr_t ptr, std::vector<unsigned int> offsets);
std::wstring ReadWString(PVOID ptr);