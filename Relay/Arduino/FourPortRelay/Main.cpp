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
	Serial.print("Major ");
	Serial.print(args.Get_Major());
	Serial.print(" Minor ");
	Serial.print(args.Get_Minor());
	Serial.print(" Value1 ");
	Serial.print(args.Get_Value1());
	Serial.print(" Value2 ");
	Serial.println(args.Get_Value2());
	if(args.Get_Major() == Get)
	{
	}
}