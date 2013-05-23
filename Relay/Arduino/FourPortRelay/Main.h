#ifndef _MAIN_H_
#define _MAIN_H_

#include "CommandProcesser.h";
#include "CommandArgs.h";
#include "ICommandReceived.h"
#include "Relay.h"

class Main : public ICommandReceived
{
public:
	Main();

	void Init(void);
	void Loop(void);

	void OnCommandReceived(CommandArgs args);
private:
	CommandProcesser processer;
	Relay relay;
};

#endif

