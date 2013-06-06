#ifndef _ICOMMANDRECEIVED_H_
#define _ICOMMANDRECEIVED_H_

#include "Common.h"
#include "CommandArgs.h"
namespace Sannel
{
	namespace Relay
	{
		namespace Command
		{
			class ICommandReceived
			{
			public:
				virtual ~ICommandReceived() {}
				virtual void OnCommandReceived(CommandArgs) = 0;
			};
		};
	};
};
#endif