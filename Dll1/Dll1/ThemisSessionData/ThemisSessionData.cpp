#include "ThemisSessionData.h"
std::wofstream Output2("C:\\ProgramData\\Thynius\\DebugSessionData");
int endMainCp = 0;
int endHeaderCp = 0;
int endFootnoteCp = 0;
bool EndCpListenerThreadReady = false;
bool HasLoggedSessionStart = false;
DWORD WINAPI EndCpListenerThread(Data* data) {
	while (true) {
		Output2 << data->GetMainDocCp() << std::endl;
		endMainCp = data->GetMainDocCp();
		endHeaderCp = data->GetHeaderCp();
		endFootnoteCp = data->GetFootnoteCp();
		Sleep(100);
	}
}
ThemisSessionData::ThemisSessionData(Data* data, std::chrono::system_clock::time_point startTime) {
	this->startTime = startTime;
	this->data = data;
	CloseHandle(CreateThread(nullptr, 0, (LPTHREAD_START_ROUTINE)EndCpListenerThread, data, 0, nullptr));
}
SessionData ThemisSessionData::GetSessionData() {
	LogInput(ActionType::ADDCHAR, L"", data->GetSelection());
	PopInput();
	SessionData toReturn;
	toReturn.inputs = inputs;
	toReturn.endMainCp = endMainCp;
	toReturn.endHeaderCp = endHeaderCp;
	toReturn.endFootnoteCp = endFootnoteCp;
	return toReturn;
}

Input ThemisSessionData::PopInput() {
	Input toReturn = this->inputs[inputs.size() - 1];
	this->inputs.pop_back();
	return toReturn;
}

void ThemisSessionData::PushInput(Input toPush) {
	this->inputs.push_back(toPush);
}

bool isUserUndoingOrRedoingUninterestingStuff(Input inputBeforeLastInput, Input lastInput) {
	return (lastInput.MainCp == inputBeforeLastInput.MainCp && (lastInput.HeaderCp == inputBeforeLastInput.HeaderCp || lastInput.HeaderCp < 14) && (lastInput.FootnoteCp == inputBeforeLastInput.FootnoteCp || lastInput.FootnoteCp < 1));
}

void ThemisSessionData::LogInput(ActionType _ActionType, std::wstring ActionContent, Selection _Selection) {
	if (!HasLoggedSessionStart) {
		LogSessionStart(this->startTime);
		HasLoggedSessionStart = true;
	}

	Input toPush;
	toPush._ActionType = _ActionType;
	toPush.ActionContent = ActionContent;
	toPush.relativeTimePointMs = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::duration<double>(std::chrono::system_clock::now() - this->startTime)).count();
	toPush._Selection = _Selection;
	toPush.MainCp = this->data->GetMainDocCp();
	toPush.HeaderCp = this->data->GetHeaderCp();
	toPush.FootnoteCp = this->data->GetFootnoteCp();

	Input lastInput = inputs[inputs.size() - 1];
	if (lastInput._ActionType == ActionType::UNDO && inputs.size() > 1) {
		Input inputBeforeLastInput = inputs[inputs.size() - 2];
		inputs.pop_back();

		if (!isUserUndoingOrRedoingUninterestingStuff(lastInput, toPush)) {
			UndoCacheItem cacheItemToPush = UndoCacheItem();
			cacheItemToPush.inputs = std::vector<Input>();

			if (inputBeforeLastInput._ActionType == ActionType::ADDCHAR) {
				int CpDiff = _abs64(lastInput.MainCp - toPush.MainCp);
				for (int i = 0; CpDiff > i; i++) {
					lastInput = inputs[inputs.size() - 1];
					cacheItemToPush.inputs.push_back(lastInput);
					inputs.pop_back();
				}

				/*If headercp is lower than 24 then the header world is not initialized.
				  it is initialized when clicked for the first time so we dont want this behavior
				  to interfere with our logic.*/
				if (!(inputBeforeLastInput.HeaderCp < 24)) {
					CpDiff = _abs64(lastInput.HeaderCp - toPush.HeaderCp);
					for (int i = 0; CpDiff > i; i++) {
						lastInput = inputs[inputs.size() - 1];
						cacheItemToPush.inputs.push_back(lastInput);
						inputs.pop_back();
					}
				}

				//Same as above but for footnote world
				if (!(inputBeforeLastInput.FootnoteCp < 1)) {
					CpDiff = _abs64(lastInput.FootnoteCp - toPush.FootnoteCp);
					for (int i = 0; CpDiff > i; i++) {
						lastInput = inputs[inputs.size() - 1];
						cacheItemToPush.inputs.push_back(lastInput);
						inputs.pop_back();
					}
				}

			}
			else {
				/*Make sure user is not undoing something else other than what we are interested in such as font changes*/
				inputs.pop_back();
				cacheItemToPush.inputs.push_back(lastInput);
			}
			undoCache.push_back(cacheItemToPush);
		}
	}
	if (lastInput._ActionType == ActionType::REDO && inputs.size() > 1) {
		inputs.pop_back();

		if (!isUserUndoingOrRedoingUninterestingStuff(lastInput, toPush)) {
			std::vector<Input> toPushInputs = undoCache[undoCache.size() - 1].inputs;
			for (int i = 0; toPushInputs.size() > i; i++) {
				inputs.push_back(toPushInputs[toPushInputs.size() - 1 - i]);
			}
			undoCache.pop_back();
		}
	}

    #if defined(ENV64BIT)
    #else
	if (lastInput._ActionType == ActionType::PASTE) {
		Input inputBeforeLastInput = inputs[inputs.size() - 2];
		if (isUserUndoingOrRedoingUninterestingStuff(inputBeforeLastInput, lastInput) ) {
			this->inputs.pop_back();
		}
	}
    #endif
	inputs.push_back(toPush);
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
	toPush.MainCp = this->data->GetMainDocCp();
	toPush.HeaderCp = this->data->GetHeaderCp();
	toPush.FootnoteCp = this->data->GetFootnoteCp();
	this->inputs.push_back(toPush);
}