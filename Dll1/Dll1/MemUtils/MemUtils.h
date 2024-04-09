#pragma once
#include <Windows.h>
#include <iostream>
#include <tlhelp32.h>
#include <Psapi.h>
#include <vector>
#include <array>

MODULEINFO GetModuleInfo(LPCWSTR szModule);
std::uint8_t* PatternScan(void* module, const char* signature);
std::wstring ReadWString(PVOID ptr);