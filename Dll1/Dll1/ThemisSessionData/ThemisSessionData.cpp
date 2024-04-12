#include "ThemisSessionData.h"
std::wofstream Output1("C:\\Program Files\\Themis\\DebugSessionData1");
std::wofstream Output2("C:\\Program Files\\Themis\\DebugSessionData2");
int endCp;
bool EndCpListenerThreadReady = false;
DWORD WINAPI EndCpListenerThread(Data* data) {
	while (true) {
		endCp = data->GetCp();
		Sleep(100);
	}
}
ThemisSessionData::ThemisSessionData(Data* data, std::chrono::system_clock::time_point startTime) {
	this->startTime = startTime;
	this->data = data;
	CloseHandle(CreateThread(nullptr, 0, (LPTHREAD_START_ROUTINE)EndCpListenerThread, data, 0, nullptr));
}
SessionData ThemisSessionData::GetSessionData() {
	SessionData toReturn;
	toReturn.inputs = inputs;
	toReturn.endCp = endCp;
	return toReturn;
}

ActionType ThemisSessionData::GetLastActionType() {
	int i = this->inputs.size() - 1;
	if (i == -1) {
		return ActionType::INVALID;
	}
	return this->inputs[i]._ActionType;
}

void ThemisSessionData::PopInput() {
	this->inputs.pop_back();
}

void ThemisSessionData::LogInput(ActionType _ActionType, std::wstring ActionContent, Selection _Selection) {
	Input toPush;
	toPush._ActionType = _ActionType;
	toPush.ActionContent = ActionContent;
	toPush.relativeTimePointMs = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::duration<double>(std::chrono::system_clock::now() - this->startTime)).count();
	toPush._Selection = _Selection;
	toPush.Cp = data->GetCp();
	this->inputs.push_back(toPush);
}

void ThemisSessionData::LogSessionStart(std::chrono::system_clock::time_point startTime) {
	Input toPush;
	toPush._ActionType = ActionType::SESSIONSTART;
	toPush.ActionContent = L"";
	toPush.relativeTimePointMs = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::duration<double>(startTime.time_since_epoch() - std::chrono::years(54))).count();
	Selection sel;
	sel.SelectionStart = 0;
	sel.SelectionEnd = 0;
	toPush._Selection = sel;
	toPush.Cp = 0;
	this->inputs.push_back(toPush);
}