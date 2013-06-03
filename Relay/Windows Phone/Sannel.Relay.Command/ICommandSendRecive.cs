using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.Command
{
	public interface ICommandSendRecive
	{
		event CommandDelegate SendCommand;

		void OnCommandRecived(Object sender, CommandArgs args);
	}
}
