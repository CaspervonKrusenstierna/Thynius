#include "SystemUtils.h"

std::wstring GetClipboardText()
{
    if (!OpenClipboard(nullptr)) {
        return NULL;
    }
 
    HANDLE hData = GetClipboardData(CF_UNICODETEXT);
    wchar_t* pszText = static_cast<wchar_t*>(GlobalLock(hData));

    std::wstring text(pszText);

    GlobalUnlock(hData);

    CloseClipboard();

    return text;
}

std::string to_utf8(std::wstring& wide_string)
{
    static std::wstring_convert<std::codecvt_utf8<wchar_t>> utf8_conv;
    return utf8_conv.to_bytes(wide_string);
}

BOOL CALLBACK EnumWindowsCallback(HWND hwnd, LPARAM lParam) {
    WindowFinderParams& params = *(WindowFinderParams*)lParam;
    
    wchar_t className[MAX_CLASSNAME] = L"";
    GetClassName(hwnd, className, MAX_CLASSNAME);
    std::wstring classNameWstr = className;
    if (params.className == classNameWstr) {
        DWORD hwndProcId;
        GetWindowThreadProcessId(
            hwnd,
            &hwndProcId
        );
        if (hwndProcId == GetCurrentProcessId()) {
            params.hwnds.push_back(hwnd);
        }
    }
}
std::vector<HWND> GetCurrProcWindowsByClassName(std::wstring className) {
    WindowFinderParams params = WindowFinderParams();
    params.className = className;
    params.hwnds = std::vector<HWND>();
    EnumWindows(EnumWindowsCallback, (LPARAM)&params);
    return params.hwnds;
}
