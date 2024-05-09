#pragma once
#include <stdio.h>
#include <string>
#include <chrono>
#include <vector>
#include <fstream>
#include "../Hooks/Hooks.h"
#include "../Data/Data.h"

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

enum ActionType {
    SESSIONSTART = 0,
    END = 1,
    INVALID = 2,
    ADDCHAR = 3,
    DELETESELECTION = 4,
    PASTE = 5,
    SPELLINGREPLACE = 6,
    INSERTIMAGE = 7,
    INSERTBREAK = 8,
    INSERTFOOTNOTE = 9,
    PASTERECENTINPUT = 10,
    REDO = 98,
    UNDO = 99
};

struct Input {
    ActionType _ActionType;
    std::wstring ActionContent;
    Selection _Selection;
    unsigned long MainCp;
    unsigned long HeaderCp;
    unsigned long FootnoteCp;
    unsigned long long relativeTimePointMs;
};

struct CpInfo {
    unsigned long MainCp;
    unsigned long HeaderCp;
    unsigned long FootnoteCp;
};

struct SessionData {
    std::vector<Input> inputs = std::vector<Input>();
    UINT64 endMainCp;
    UINT64 endHeaderCp;
    UINT64 endFootnoteCp;
};

struct UndoCacheItem {
    std::vector<Input> inputs;
};

class ThemisSessionData {

    public:
        ThemisSessionData(Data* data, std::chrono::system_clock::time_point startTime);
        SessionData GetSessionData();
        void LogInput(ActionType _ActionType, std::wstring ActionContent, Selection _Selection);
        Input GetLastInput();
        Input PopInput();
        void PushInput(Input toPush);
        int GetHighestCpDiffFromInput(Input input);
        int endMainCp = 0;
        int endHeaderCp = 0;
        int endFootnoteCp = 0;
        Data* data;
    private:
        std::vector<UndoCacheItem> undoCache = std::vector<UndoCacheItem>();
        std::vector<Input> inputs = std::vector<Input>();
        void LogSessionStart(std::chrono::system_clock::time_point startTime);
        std::chrono::system_clock::time_point startTime;
};