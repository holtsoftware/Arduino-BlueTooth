#include "Main.h"

Main::Main() : relay()
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