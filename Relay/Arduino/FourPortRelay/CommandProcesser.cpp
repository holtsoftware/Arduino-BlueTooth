#include "CommandProcesser.h"
#include "CommandArgs.h"
#include <string.h>

CommandProcesser::CommandProcesser() : ss(2, 3), index(0)
{
}

void CommandProcesser::SetCommandReceived(ICommandReceived* received)
{
	this->received = received;
}

void CommandProcesser::Process()
{
	while(this->ss.available())
	{
		byte b = ss.read();
		buffer[index] = b;
		index ++;
		if(index >= 13)
		{
			SendCommand();
			index = 0;
		}
	}
}

void CommandProcesser::SendCommand()
{
	CommandArgs args;
	if(buffer[0] == 0)
	{
		args.Set_Major(Get);
	}
	else if(buffer[0] == 1)
	{
		args.Set_Major(Set);
	}

	int minor = ((int)buffer[1]<<24)|((int)buffer[2]<<16)|((int)buffer[3] << 8)|(int)buffer[4];
	args.Set_Minor(minor);

	int value = ((int)buffer[5]<<24)|((int)buffer[6]<<16)|((int)buffer[7] << 8)|((int)buffer[8]);
	args.Set_Value1(value);

	value = ((int)buffer[9]<<24)|((int)buffer[10]<<16)|((int)buffer[11] << 8)| ((int)buffer[12]);
	args.Set_Value2(value);

	if(this->received)
	{
		this->received->OnCommandReceived(args);
	}
}