#ifndef _COMMANDARGS_H_
#define _COMMANDARGS_H_

#include "Common.h"
#include "CommandType.h"
#include "Command.h"

namespace Sannel
{
	namespace Relay
	{
		namespace Command
		{

			class CommandArgs
			{
			public:
				CommandArgs();

				CommandType Get_Type();
				void Set_Type(CommandType);

				Command Get_Command();
				void Set_Command(Command);

				byte* Get_Value();
				void Set_Value(byte* value);
			private:
				CommandType type;
				Command command;
				byte* value;
			};
		};
	};
};
#endif