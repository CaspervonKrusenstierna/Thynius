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
