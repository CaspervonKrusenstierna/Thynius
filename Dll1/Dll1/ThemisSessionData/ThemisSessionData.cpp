#include "ThemisSessionData.h"
std::wofstream Output1("C:\\Program Files\\Themis\\DebugSessionData1");
std::wofstream Output2("C:\\Program Files\\Themis\\DebugSessionData2");


ThemisSessionData::ThemisSessionData(Data* data) {
    this->startTime = std::chrono::system_clock::now();
	this->data = data;
}

SessionData ThemisSessionData::GetSessionData() {
	SessionData toReturn;
	toReturn.inputs = inputs;
	toReturn.endCp = data->GetCp();
	return toReturn;
}

ActionType ThemisSessionData::GetLastActionType() {
	int i = this->inputs.size() - 1;
	int UndoDepthCopy = this->UndoDepth;
	while (true) {
		if (0 > i) {
			return ActionType::INVALID;
		}
		Input currInput = inputs[i];
		ActionType currInputActionType = currInput._ActionType;
		if (currInputActionType != ActionType::UNDO && currInputActionType != ActionType::REDO) {
			if (UndoDepthCopy == 0) {
				return currInputActionType;
			}
			UndoDepthCopy--;
		}
	}
}

void ThemisSessionData::IncUndoDepth() {
	UndoDepth++;
}

void ThemisSessionData::DecUndoDepth() {
	UndoDepth--;
}

void ThemisSessionData::LogInput(ActionType _ActionType, std::wstring ActionContent, Selection _Selection) {
	Input toPush;
	toPush._ActionType = _ActionType;
	toPush.ActionContent = ActionContent;
	toPush.relativeTimePointMs = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::duration<double>(std::chrono::system_clock::now() - this->startTime)).count();
	toPush._Selection = _Selection;
	toPush.Cp = data->GetCp();
	this->inputs.push_back(toPush);
	this->UndoDepth = 0;
}