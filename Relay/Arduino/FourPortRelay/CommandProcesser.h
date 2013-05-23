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

protected:
	void SendCommand();

private:
	SoftwareSerial ss;
	ICommandReceived* received;
	byte buffer[13];
	int index;
};

#endif