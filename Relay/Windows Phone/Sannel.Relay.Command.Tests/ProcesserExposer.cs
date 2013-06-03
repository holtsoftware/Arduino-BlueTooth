using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.Command.Tests
{
	public class ProcesserExposer : Processer
	{
		public CommandArgs ReadPacketExposed(Stream stream)
		{
			return base.ReadPacket(stream);
		}

		public byte[] GeneratePackageExposed(CommandArgs args)
		{
			return base.GeneratePackage(args);
		}
	}
}
