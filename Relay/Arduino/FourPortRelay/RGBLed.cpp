#include "RGBLed.h"

using namespace Sannel::Relay;
using namespace Sannel::Relay::Command;

RGBLed::RGBLed(ICommandReceived* receiver) :
	red(0),
	green(0),
	blue(0)
{
	this->receiver = receiver;

	pinMode(REDPIN, OUTPUT);
	pinMode(GREENPIN, OUTPUT);
	pinMode(BLUEPIN, OUTPUT);
	analogWrite(REDPIN, 0);
	analogWrite(GREENPIN, 0);
	analogWrite(BLUEPIN, 0);
}

byte RGBLed::Get_Red()
{
	return this->red;
}

void RGBLed::Set_Red(byte red)
{
	this->red = red;
	analogWrite(REDPIN, this->red);
}

byte RGBLed::Get_Green()
{
	return this->green;
}

void RGBLed::Set_Green(byte green)
{
	this->green = green;
	analogWrite(GREENPIN, this->green);
}

byte RGBLed::Get_Blue()
{
	return this->blue;
}

void RGBLed::Set_Blue(byte blue)
{
	this->blue = blue;
	analogWrite(BLUEPIN, this->blue);
}

void RGBLed::OnCommandReceived(CommandArgs *args)
{
	if(args->Get_Type() == Get && (args->Get_Command() == All | args->Get_Command() == RGBLedCommand))
	{
		CommandArgs sendArgs;
		sendArgs.Set_Type(Set);
		sendArgs.Set_Command(RGBLedCommand);
		byte values[3];
		values[0] = Get_Red();
		values[1] = Get_Green();
		values[2] = Get_Blue();
		SendCommand(&sendArgs);
	}
	else if(args->Get_Type() == Set && args->Get_Command() == RGBLedCommand)
	{
		byte* values = args->Get_Value();
		if(values != null)
		{
			if(sizeof(values) == 3)
			{
				Set_Red(values[0]);
				Set_Green(values[1]);
				Set_Blue(values[2]);
			}
		}
	}
}

void RGBLed::SendCommand(CommandArgs* args)
{
	if(this->receiver != null)
	{
		this->receiver->SendCommand(args);
	}
}