#include <iostream>
#include <Windows.h>
#include <codecvt>
#include <chrono>
#include <locale> 
#pragma warning(disable : 4996).

std::wstring GetClipboardText();
std::string gen_random(const int len);
std::string to_utf8(std::wstring& wide_string);

struct timeInfo {
	std::chrono::year year;
	std::chrono::month month;
	std::chrono::day day;
};