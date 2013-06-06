#include "CommandArgs.h"

using namespace Sannel::Relay::Command;

CommandArgs::CommandArgs()
{
}

CommandType CommandArgs::Get_Type()
{
	return this->type;
}

void CommandArgs::Set_Type(CommandType type)
{
	this->type = type;
}

Command CommandArgs::Get_Command()
{
	return this->command;
}

void CommandArgs::Set_Command(Command com)
{
	this->command = com;
}

byte* CommandArgs::Get_Value()
{
	return this->value;
}

void CommandArgs::Set_Value(byte* val)
{
	this->value = val;
}