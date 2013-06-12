using Sannel.Relay.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Core;

namespace Sannel.Relay.ViewModels
{
	public class RelayViewModel : Sannel.ComponentModel.NotifyPropertyChangedBase, ICommandSendRecive
	{
		public event CommandDelegate SendCommand;

		public async void OnCommandRecived(object sender, CommandArgs args)
		{
			if (args.Type == CommandType.Set && args.Command == Command.Command.Relay)
			{
				byte[] value = args.Value;
				if (value != null && value.Length == 1)
				{
					byte b = value[0];
					int relay = (b >> 4);
					int val = b & 1;
					var dispatcher = Deployment.Current.Dispatcher;

					switch(relay)
					{
						case 1:
							relay1 = val == 1;
							dispatcher.BeginInvoke(() =>
								{
									OnPropertyChanged("Relay1");
								});
							break;
						case 2:
							relay2 = val == 1;
							dispatcher.BeginInvoke(() =>
							{
								OnPropertyChanged("Relay2");
							});
							break;
						case 3:
							relay3 = val == 1;
							dispatcher.BeginInvoke(() =>
							{
								OnPropertyChanged("Relay3");
							});
							break;
						case 4:
							relay4 = val == 1;
							dispatcher.BeginInvoke(() =>
							{
								OnPropertyChanged("Relay4");
							});
							break;
						default:
							break;
					}
				}
			}
		}

		protected void FireSendCommand(byte relay, bool value)
		{
			if (SendCommand != null)
			{
				lock (SendCommand)
				{
					CommandArgs args = new CommandArgs();
					args.Type = CommandType.Set;
					args.Command = Command.Command.Relay;
					args.Value = new byte[] { (byte)((relay << 4) | ((value)?1:0)) };
					SendCommand(this, args);
				}
			}
		}

		private bool relay1;
		public bool Relay1
		{
			get
			{
				return relay1;
			}
			set
			{
				SetProperty(ref relay1, value, "Relay1");
				FireSendCommand(1, value);
			}
		}

		private bool relay2;
		public bool Relay2
		{
			get
			{
				return relay2;
			}
			set
			{
				SetProperty(ref relay2, value, "Relay2");
				FireSendCommand(2, value);
			}
		}

		private bool relay3;
		public bool Relay3
		{
			get
			{
				return relay3;
			}
			set
			{
				SetProperty(ref relay3, value, "Relay3");
				FireSendCommand(3, value);
			}
		}

		private bool relay4;
		public bool Relay4
		{
			get
			{
				return relay4;
			}
			set
			{
				SetProperty(ref relay4, value, "Relay4");
				FireSendCommand(4, value);
			}
		}
	}
}
