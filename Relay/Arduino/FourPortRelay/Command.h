#ifndef _COMMAND_H_
#define _COMMAND_H_

namespace Sannel
{
	namespace Relay
	{
		namespace Command
		{
			enum Command
			{
				NoCommand = 0,
				All = 1,
				RelayCommand = 2,
				RGBLedCommand = 3
			};
		};
	};
};

#endif