#include "ThemisSessionData.h"

ThemisSessionData::ThemisSessionData() {
    this->startTime = std::chrono::system_clock::now();
}

SessionData ThemisSessionData::GetSessionData() {
	return this->sessionData;
}

void ThemisSessionData::LogInput(ActionType _ActionType, std::wstring ActionContent, Selection _Selection) {
	Input toPush;
	toPush._ActionType = _ActionType;
	toPush.ActionContent = ActionContent;
	toPush.relativeTimePointMs = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::duration<double>(std::chrono::system_clock::now() - this->startTime)).count();
	toPush._Selection = _Selection;
	this->sessionData.SessionInputs.push_back(toPush);
}