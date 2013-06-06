#ifndef _PROCESSER_H_
#define _PROCESSER_H_

#include "Common.h"
#include <SoftwareSerial.h>
#include "CommandArgs.h"
#include "ICommandReceived.h"
#define StartByte 254
#define EndByte 255

namespace Sannel
{
	namespace Relay
	{
		namespace Command
		{
			class Processer
			{
			public:
				Processer();
				~Processer();

				void ProcessInput();
				void SendCommand(CommandArgs args);

				void Set_CommandReceived(ICommandReceived* receiver);

			private:
				SoftwareSerial serial;
				ICommandReceived* receiver;
				static byte packetBuffer[255];

				Int16 index;

				void processPacket();
			};
		}
	};
};
#endif