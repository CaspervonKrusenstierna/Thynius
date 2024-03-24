#include "SystemUtils.h"

std::string GetClipboardText()
{
    if (!OpenClipboard(nullptr)) {
        return NULL;
    }

    HANDLE hData = GetClipboardData(CF_TEXT);
    char* pszText = static_cast<char*>(GlobalLock(hData));

    std::string text(pszText);

    GlobalUnlock(hData);

    CloseClipboard();

    return text;
}