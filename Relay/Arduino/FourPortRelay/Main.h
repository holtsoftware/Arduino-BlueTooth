#ifndef _MAIN_H_
#define _MAIN_H_

#include "Common.h"
#include "ICommandReceived.h"
#include "CommandArgs.h"
#include "Relay4Port.h"
#include "RGBLed.h"
#include "Processer.h"
#include "Buttons4.h"

using namespace Sannel::Relay::Command;

namespace Sannel
{
	namespace Relay
	{
		class Main : public ICommandReceived
		{
		public:
			Main();

			void Init(void);
			void Loop(void);

			void OnCommandReceived(CommandArgs*);
			void SendCommand(CommandArgs*);
		
		private:
			Relay4Port relay;
			RGBLed led;
			Processer processer;
			Buttons4 buttons;
		};
	};
};

#endif

