#include <stdio.h>
#include <string>
#include <chrono>
#include <vector>
#include "../Data/Data.h"

enum ActionType {
    ADDCHAR = 0,
    DELETESELECTION = 1,
    PASTE = 2
};

struct Input {
    ActionType _ActionType;
    std::wstring ActionContent;
    Selection _Selection;
    unsigned long long relativeTimePointMs;
};

struct SessionData {
    std::vector<Input> SessionInputs;
};

class ThemisSessionData {

    public:
        ThemisSessionData();
        SessionData GetSessionData();
        void LogInput(ActionType _ActionType, std::wstring ActionContent, Selection _Selection);
    private:
        SessionData sessionData;
        std::chrono::system_clock::time_point startTime;

};