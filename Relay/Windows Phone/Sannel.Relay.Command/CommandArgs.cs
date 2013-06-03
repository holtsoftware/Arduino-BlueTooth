using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.Command
{
	public class CommandArgs
	{
		public CommandType Type
		{
			get;
			set;
		}

		public Command Command
		{
			get;
			set;
		}

		public byte[] Value
		{
			get;
			set;
		}
	}
}
