#include "Main.h"

using namespace Sannel::Relay;
using namespace Sannel::Relay::Command;

Main::Main() :
	relay(this),
	led(this),
	buttons(&relay)
{
	processer.Set_CommandReceived(this);
}

void Main::Init(void)
{
	
}

void Main::Loop(void)
{
	processer.ProcessInput();
	buttons.CheckButtons();
}

void Main::OnCommandReceived(CommandArgs* args)
{
	relay.OnCommandReceived(args);
	led.OnCommandReceived(args);
}

void Main::SendCommand(CommandArgs* args)
{
	processer.SendCommand(args);
}