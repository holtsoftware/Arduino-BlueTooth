#ifndef _RELAY_H_
#define _RELAY_H_

#include "Common.h"
#include "ICommandReceived.h"

using namespace Sannel::Relay::Command;

namespace Sannel
{
	namespace Relay
	{
		class Relay4Port : public ICommandReceived
		{
		public:
			Relay4Port(ICommandReceived*);

			bool Get_Relay1();
			void Set_Relay1(bool);

			bool Get_Relay2();
			void Set_Relay2(bool);

			bool Get_Relay3();
			void Set_Relay3(bool);

			bool Get_Relay4();
			void Set_Relay4(bool);

			void OnCommandReceived(CommandArgs*);
			void SendCommand(CommandArgs*);

		protected:
			void WritePin(byte pin, bool onOff);
			void SendSetPacket(byte relayNum, bool onOff);

		private:
			bool relay1;
			bool relay2;
			bool relay3;
			bool relay4;
			ICommandReceived* sender;
		};
	};
};
#endif