#ifndef _COMMANDPROCESSER_H_
#define _COMMANDPROCESSER_H_
#include <Arduino.h>
#include <SoftwareSerial.h>
#include "CommandArgs.h"

class CommandProcesser
{
public:
	CommandProcesser();

	void Process();
	void SetCommandRecived(void (*callback)(CommandArgs));

private:
	SoftwareSerial ss;
	void (*commandRecivedCallBack)(CommandArgs);
	byte buffer[9];
	int index;
};

#endif