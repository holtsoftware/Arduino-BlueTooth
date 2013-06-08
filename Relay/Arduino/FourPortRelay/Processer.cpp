#include "Processer.h"

using namespace Sannel::Relay::Command;

byte Processer::packetBuffer[255];
byte Processer::valueBuffer[243];
byte Processer::index = -1;

Processer::Processer() : serial(2, 3)
{
}

Processer::~Processer()
{
}

void Processer::ProcessInput()
{
	if(serial.available())
	{
		if(index > -1)
		{
			processPacket();
		}
		else
		{
			Int16 val;
			while(serial.available())
			{
				val = serial.read();
				if(val == StartByte)
				{
					index = 0;
					crc = 255;
					processPacket();
				}
			}
		}
	}
}

void Processer::processPacket()
{
	Int16 val;
	while(serial.available())
	{
		val = serial.read();
		
		packetBuffer[this->index] = (byte)val;
		if(index > 0 && index == (length - 9))
		{
			if(packetBuffer[index] == crc)
			{
				void* value = &valueBuffer[(243 - (length - 10))];
				const void* packetStart = &packetBuffer[9];
				memcpy(value, packetStart, length - 10);

				CommandArgs args;
				args.Set_Type((CommandType)type);
				args.Set_Command((Command)command);
				args.Set_Value((byte*)value);

				this->receiver->OnCommandReceived(&args);
				index = -1;
				return;
			}
			else
			{
				// Invalid crc throw out the packet.
				index = -1;
				return;
			}
		}
		else
		{
			crc = crc ^ val;
			if(index == 0)
			{
				length = val;
			}
			if(index == 4)
			{
				type = packetBuffer[index];
			}
			if(index == 8)
			{
				command = packetBuffer[index];
			}
		}
		

		index++;
	}
}

void Processer::Set_CommandReceived(ICommandReceived* receiver)
{
	this->receiver = receiver;
}

// [1 byte (start) (254)][1 byte (Length in bytes max 253)][4 bytes (CommandType)][4 bytes (Command)][0-243 bytes (Data)][1 byte (CRC)][1 byte (stop) (255)]
void Processer::SendCommand(CommandArgs* args)
{
	byte valueLength = sizeof(args->Get_Value());
	if(valueLength > 243)
	{
		return; // not currently supporting multiple packets so dump this package as invalid.
	}
	serial.write(StartByte);
	byte temp;
	byte crc = 255;
	temp = 10 + valueLength;
	serial.write(temp);
	crc = crc ^ temp;
	// The packet requires Int32 for Type but we don't even fill up a byte with possible types so the first 3 bytes are set to zero
	temp = 0;
	serial.write(temp);
	crc = crc ^ temp;
	serial.write(temp);
	crc = crc ^ temp;
	serial.write(temp);
	crc = crc ^ temp;
	temp = (byte)args->Get_Type();
	serial.write(temp);
	crc = crc ^ temp;
	// The packet requires Int32 for Command but we don't even fill up a byte with possible Commands so the first 3 bytes are set to zero
	temp = 0;
	serial.write(temp);
	crc = crc ^ temp;
	serial.write(temp);
	crc = crc ^ temp;
	serial.write(temp);
	crc = crc ^ temp;
	temp = (byte)args->Get_Command();
	serial.write(temp);
	crc = crc ^ temp;

	byte* value = args->Get_Value();

	for(byte i=0;i< valueLength;i++)
	{
		temp = value[i];
		serial.write(temp);
		crc = crc ^ temp;
	}

	serial.write(crc);
	serial.write(EndByte);
}
