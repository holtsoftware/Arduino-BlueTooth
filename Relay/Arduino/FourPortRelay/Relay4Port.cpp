#include "Relay4Port.h"


#define RELAY1_PIN		7
#define RELAY2_PIN		5
#define RELAY3_PIN		6
#define RELAY4_PIN		4

using namespace Sannel::Relay;
using namespace Sannel::Relay::Command;

Relay4Port::Relay4Port(ICommandReceived* sender) :
	relay1(false),
	relay2(false),
	relay3(false),
	relay4(false)
{
	pinMode(RELAY1_PIN, OUTPUT);
	pinMode(RELAY2_PIN, OUTPUT);
	pinMode(RELAY3_PIN, OUTPUT);
	pinMode(RELAY4_PIN, OUTPUT);
	digitalWrite(RELAY1_PIN, HIGH);
	digitalWrite(RELAY2_PIN, HIGH);
	digitalWrite(RELAY3_PIN, HIGH);
	digitalWrite(RELAY4_PIN, HIGH);
	this->sender = sender;
}

bool Relay4Port::Get_Relay1()
{
	return relay1;
}

void Relay4Port::Set_Relay1(bool on)
{
	relay1 = on;
	WritePin(RELAY1_PIN, on);
	SendSetPacket(1, on);
}

bool Relay4Port::Get_Relay2()
{
	return relay2;
}

void Relay4Port::Set_Relay2(bool on)
{
	relay2 = on;
	WritePin(RELAY2_PIN, on);
	SendSetPacket(2, on);
}

bool Relay4Port::Get_Relay3()
{
	return relay3;
}

void Relay4Port::Set_Relay3(bool on)
{
	relay3 = on;
	WritePin(RELAY3_PIN, on);
	SendSetPacket(3, on);
}

bool Relay4Port::Get_Relay4()
{
	return relay4;
}

void Relay4Port::Set_Relay4(bool on)
{
	relay4 = on;
	WritePin(RELAY4_PIN, on);
	SendSetPacket(4, on);
}

void Relay4Port::OnCommandReceived(CommandArgs* args)
{
	if(args->Get_Type() == Get && (args->Get_Command() == All || args->Get_Command() == RelayCommand))
	{
		CommandArgs sendArgs;
		sendArgs.Set_Type(Set);
		sendArgs.Set_Command(RelayCommand);
		sendArgs.Set_Length(1);
		byte value[1];
		value[0] = (1 << 4) | Get_Relay1();
		sendArgs.Set_Value(value);
		SendCommand(&sendArgs);
		value[0] = (2 << 4) | Get_Relay2();
		sendArgs.Set_Value(value);
		SendCommand(&sendArgs);
		value[0] = (3 << 4) | Get_Relay3();
		sendArgs.Set_Value(value);
		SendCommand(&sendArgs);
		value[0] = (4 << 4) | Get_Relay4();
		SendCommand(&sendArgs);
	}
	else if(args->Get_Type() == Set && args->Get_Command() == RelayCommand)
	{
		byte* value = args->Get_Value();
		if(value != null)
		{
			if(args->Get_Length() == 1)
			{
				byte relay = value[0] >> 4;
				byte onOff = value[0] & 1;
				switch(relay)
				{
				case 1:
					relay1 = onOff == 1;
					WritePin(RELAY1_PIN, relay1);
					break;

				case 2:
					relay2 = onOff == 1;
					WritePin(RELAY2_PIN, relay2);
					break;

				case 3:
					relay3 = onOff == 1;
					WritePin(RELAY3_PIN, relay3);
					break;

				case 4:
					relay4 = onOff == 1;
					WritePin(RELAY4_PIN, relay4);
					break;

				default:
					break;
				}
			}
		}
	}
}

void Relay4Port::SendCommand(CommandArgs* args)
{
	if(this->sender != null)
	{
		this->sender->SendCommand(args);
	}
}

void Relay4Port::WritePin(byte pin, bool onOff)
{
	digitalWrite(pin, (onOff)?LOW:HIGH);
}

void Relay4Port::SendSetPacket(byte number, bool onOff)
{
	CommandArgs args;
	args.Set_Type(Set);
	args.Set_Command(RelayCommand);
	byte value[1];
	value[0] = (number << 4) | onOff;
	args.Set_Length(1);
	args.Set_Value(value);
	SendCommand(&args);
}