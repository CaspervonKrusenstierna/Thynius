#pragma once
#include <iostream>
#include <Windows.h>
#include <codecvt>
#include <chrono>
#include <locale> 
#pragma warning(disable : 4996).

#define MAX_CLASSNAME 255

std::wstring GetClipboardText();
std::string to_utf8(std::wstring& wide_string);
std::vector<HWND> GetCurrProcWindowsByClassName(std::wstring className);

struct timeInfo {
	std::chrono::year year;
	std::chrono::month month;
	std::chrono::day day;
};

struct WindowFinderParams {
	std::wstring className;
	std::vector<HWND> hwnds;
};