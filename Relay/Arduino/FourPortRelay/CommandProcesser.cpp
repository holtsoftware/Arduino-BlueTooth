#include "CommandProcesser.h"
#include "CommandArgs.h"
#include <string.h>
#include "Common.h"
#include "BitConverter.h"

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
		if(index >= 8)
		{
			OnCommandRecived();
			index = 0;
		}
	}
}

void CommandProcesser::SendCommand(CommandArgs args)
{
	ss.write((byte)0);
	ss.write((byte)args.Get_Major());

	byte* bytes = System::BitConverter::GetBytes(args.Get_Minor());

	sendIntBytes(bytes);

	bytes = System::BitConverter::GetBytes(args.Get_Value1());

	sendIntBytes(bytes);

	bytes = System::BitConverter::GetBytes(args.Get_Value2());

	sendIntBytes(bytes);

}

void CommandProcesser::OnCommandRecived()
{
	CommandArgs args;
	if(buffer[1] == 0)
	{
		args.Set_Major(Get);
	}
	else if(buffer[1] == 1)
	{
		args.Set_Major(Set);
	}

	byte* ptr = buffer;

	Int16 minor = System::BitConverter::ToInt16(buffer, (Int16)2);
	args.Set_Minor(minor);

	Int16 value = System::BitConverter::ToInt16(buffer, 4);
	args.Set_Value1(value);

	value = System::BitConverter::ToInt16(buffer, 6);
	args.Set_Value2(value);

	if(this->received)
	{
		this->received->OnCommandReceived(args);
	}
}

void CommandProcesser::sendIntBytes(byte* arr)
{
	if(arr != NULL && sizeof(arr) == 4)
	{
		ss.write(arr[0]);
		ss.write(arr[1]);
	}
	else
	{
		ss.write((byte)0);
		ss.write((byte)0);
	}
}