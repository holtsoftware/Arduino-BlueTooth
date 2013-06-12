#ifndef _BUTTONS4_H_
#define _BUTTONS4_H_

#include "Common.h"
#include "Relay4Port.h"

namespace Sannel
{
	namespace Relay
	{
		class Buttons4
		{
		public:
			Buttons4(Relay4Port *relay);

			void CheckButtons();

		private:
			Relay4Port* relay;
			bool button1Last;
			bool button2Last;
			bool button3Last;
			bool button4Last;
		};
	};
};
#endif