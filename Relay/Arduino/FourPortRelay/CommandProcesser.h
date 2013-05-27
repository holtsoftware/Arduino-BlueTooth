#ifndef _COMMANDPROCESSER_H_
#define _COMMANDPROCESSER_H_
#include <Arduino.h>
#include <SoftwareSerial.h>
#include "CommandArgs.h"
#include "ICommandReceived.h"

class CommandProcesser
{
public:
	CommandProcesser();

	void Process();
	void SetCommandReceived(ICommandReceived *);

	void SendCommand(CommandArgs);

protected:
	void OnCommandRecived();

private:
	void sendIntBytes(byte*);
	SoftwareSerial ss;
	ICommandReceived* received;
	byte buffer[8];
	int index;
};

#endif