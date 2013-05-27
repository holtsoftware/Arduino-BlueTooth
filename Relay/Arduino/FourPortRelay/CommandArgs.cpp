#include "CommandArgs.h"

CommandArgs::CommandArgs()
{
}

CommandArgs::CommandArgs(Major major, Int16 minor, Int16 value1, Int16 value2)
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

Int16 CommandArgs::Get_Minor()
{
	return this->minorCommand;
}

void CommandArgs::Set_Minor(Int16 minor)
{
	this->minorCommand = minor;
}

Int16 CommandArgs::Get_Value1()
{
	return this->value1;
}

void CommandArgs::Set_Value1(Int16 value)
{
	this->value1 = value;
}

Int16 CommandArgs::Get_Value2()
{
	return this->value2;
}

void CommandArgs::Set_Value2(Int16 value)
{
	this->value2 = value;
}
