#pragma once
#include <stdio.h>
#include <string>
#include <chrono>
#include <vector>
#include <fstream>
#include "../Hooks/Hooks.h"
#include "../Data/Data.h"

enum ActionType {
    ADDCHAR = 0,
    DELETESELECTION = 1,
    PASTE = 2,
    UNDO = 3,
    REDO = 4,
    END = 5
};

struct Input {
    ActionType _ActionType;
    std::wstring ActionContent;
    Selection _Selection;
    unsigned long Cp;
    unsigned long long relativeTimePointMs;
};

struct SessionData {
    std::vector<Input> inputs = std::vector<Input>();
    UINT64 endCp;
};
class ThemisSessionData {

    public:
        ThemisSessionData(Data* data);
        SessionData GetSessionData();
        void LogInput(ActionType _ActionType, std::wstring ActionContent, Selection _Selection);
    private:
        std::vector<Input> inputs = std::vector<Input>();
        Data* data;
        std::chrono::system_clock::time_point startTime;

};