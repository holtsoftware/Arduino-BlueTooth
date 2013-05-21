#include "CommandProcesser.h"

CommandProcesser::CommandProcesser() : ss(2, 3), index(0)
{
}

void CommandProcesser::SetCommandRecived(void (*callback)(CommandArgs))
{
	this->commandRecivedCallBack = callback;
}

void CommandProcesser::Process()
{
}