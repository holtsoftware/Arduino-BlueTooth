using Sannel.Relay.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.ViewModels
{
	public class RGBLedViewModel : Sannel.ComponentModel.NotifyPropertyChangedBase, ICommandSendRecive
	{
		public event CommandDelegate SendCommand;

		public void OnCommandRecived(object sender, CommandArgs args)
		{
			
		}
	}
}
