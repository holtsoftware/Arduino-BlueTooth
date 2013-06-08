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
	digitalWrite(RELAY1_PIN, (on)?HIGH:LOW);
}

bool Relay4Port::Get_Relay2()
{
	return relay2;
}

void Relay4Port::Set_Relay2(bool on)
{
	relay2 = on;
	digitalWrite(RELAY2_PIN, (on)?HIGH:LOW);
}

bool Relay4Port::Get_Relay3()
{
	return relay3;
}

void Relay4Port::Set_Relay3(bool on)
{
	relay3 = on;
	digitalWrite(RELAY3_PIN, (on)?HIGH:LOW);
}

bool Relay4Port::Get_Relay4()
{
	return relay4;
}

void Relay4Port::Set_Relay4(bool on)
{
	relay4 = on;
	digitalWrite(RELAY4_PIN, (on)?HIGH:LOW);
}

void Relay4Port::OnCommandReceived(CommandArgs* args)
{
	if(args->Get_Type() == Get && (args->Get_Command() == All || args->Get_Command() == RelayCommand))
	{
		CommandArgs sendArgs;
		sendArgs.Set_Type(Set);
		sendArgs.Set_Command(RelayCommand);
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
			if(sizeof(value) == 1)
			{
				byte relay = value[0] >> 4;
				byte onOff = value[0] & 1;
				switch(relay)
				{
				case 1:
					Set_Relay1(onOff == 1);
					break;

				case 2:
					Set_Relay2(onOff == 2);
					break;

				case 3:
					Set_Relay3(onOff == 3);
					break;

				case 4:
					Set_Relay4(onOff == 4);
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