#include "Processer.h"

using namespace Sannel::Relay::Command;

Processer::Processer() : serial(2, 3), index(-1)
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
					processPacket();
				}
			}
		}
	}
}

// [1 byte (start) (254)][1 byte (Length in bytes max 253)][4 bytes (CommandType)][4 bytes (Command)][0-243 bytes (Data)][1 byte (CRC)][1 byte (stop) (255)]
void Processer::SendCommand(CommandArgs args)
{
	byte valueLength = sizeof(args.Get_Value());
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
	temp = (byte)args.Get_Type();
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
	temp = (byte)args.Get_Command();
	serial.write(temp);
	crc = crc ^ temp;

	byte* value = args.Get_Value();

	for(byte i=0;i< valueLength;i++)
	{
		temp = value[i];
		serial.write(temp);
		crc = crc ^ temp;
	}

	serial.write(crc);
	serial.write(EndByte);
}