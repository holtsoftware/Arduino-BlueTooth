#include "CommandArgs.h"

CommandArgs::CommandArgs()
{
}

CommandArgs::CommandArgs(Major major, int minor, int value1, int value2)
{
	this->majorCommand = major;
	this->minorCommand = minor;
	this->value1 = value1;
	this->value2 = value2;
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

int CommandArgs::Get_Value1()
{
	return this->value1;
}

void CommandArgs::Set_Value1(int value)
{
	this->value1 = value;
}

int CommandArgs::Get_Value2()
{
	return this->value2;
}

void CommandArgs::Set_Value2(int value)
{
	this->value2 = value;
}
