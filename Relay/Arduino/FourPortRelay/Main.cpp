#include "Main.h"

Main::Main() : relay(),
	led(11, 9, 10)
{
}

void Main::Init(void)
{
	this->processer.SetCommandReceived(this);
}

void Main::Loop(void)
{
	this->processer.Process();
}

void Main::OnCommandReceived(CommandArgs args)
{

}