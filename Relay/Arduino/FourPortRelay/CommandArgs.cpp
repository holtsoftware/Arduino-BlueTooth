#include "CommandArgs.h"

CommandArgs::CommandArgs()
{
}

CommandArgs::CommandArgs(Major major, int minor, int value)
{
	this->majorCommand = major;
	this->minorCommand = minor;
	this->value = value;
}

Major CommandArgs::Get_Major()
{
	return this->majorCommand;
}

void CommandArgs::Set_Major(Major major)
{
	this->majorCommand = major;
}

int CommandArgs::Get_Minor()
{
	return this->minorCommand;
}

void CommandArgs::Set_Minor(int minor)
{
	this->minorCommand = minor;
}

int CommandArgs::Get_Value()
{
	return this->value;
}

void CommandArgs::Set_Value(int value)
{
	this->value = value;
}