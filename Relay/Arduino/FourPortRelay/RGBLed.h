#ifndef _RGBLED_H_
#define _RGBLED_H_

#include "Common.h"
#include "ICommandReceived.h"
#include "CommandArgs.h"

using namespace Sannel::Relay::Command;

#define REDPIN		11
#define GREENPIN	9
#define BLUEPIN		10

namespace Sannel
{
	namespace Relay
	{
		class RGBLed : public ICommandReceived
		{
		public:
			RGBLed(ICommandReceived* receiver);

			byte Get_Red();
			void Set_Red(byte value);

			byte Get_Green();
			void Set_Green(byte value);

			byte Get_Blue();
			void Set_Blue(byte value);

			void OnCommandReceived(CommandArgs *args);

			void SendCommand(CommandArgs * args);

		private:
			byte red;
			byte green;
			byte blue;
			ICommandReceived* receiver;
		};
	};
};
#endif