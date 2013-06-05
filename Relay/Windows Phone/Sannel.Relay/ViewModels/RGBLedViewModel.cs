using Sannel.Relay.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Sannel.Relay.ViewModels
{
	public class RGBLedViewModel : Sannel.ComponentModel.NotifyPropertyChangedBase, ICommandSendRecive
	{
		public event CommandDelegate SendCommand;

		public void OnCommandRecived(object sender, CommandArgs args)
		{
			if (args.Type == CommandType.Set && args.Command == Command.Command.RGBLed)
			{
				if (args.Value != null && args.Value.Length == 3)
				{
					red = args.Value[0];
					green = args.Value[1];
					blue = args.Value[2];
					OnPropertyChanged("Red");
					OnPropertyChanged("Green");
					OnPropertyChanged("Blue");
				}
			}
		}

		protected void FireSendCommand()
		{
			if (SendCommand != null)
			{
				lock (SendCommand)
				{
					CommandArgs args = new CommandArgs();
					args.Type = CommandType.Set;
					args.Command = Command.Command.RGBLed;
					args.Value = new byte[] { red, green, blue };
					SendCommand(this, args);
				}
			}
		}

		private byte red = 0;
		public byte Red
		{
			get
			{
				return red;
			}
			set
			{
				SetProperty(ref red, value, "Red");
				FireSendCommand();
			}
		}

		private byte green = 0;
		public byte Green
		{
			get
			{
				return green;
			}
			set
			{
				SetProperty(ref green, value, "Green");
				FireSendCommand();
			}
		}

		private byte blue = 0;
		public byte Blue
		{
			get
			{
				return blue;
			}
			set
			{
				SetProperty(ref blue, value, "Blue");
				FireSendCommand();
			}
		}

		public Color Color
		{
			get
			{
				return Color.FromArgb(255, red, green, blue); 
			}
			set
			{
				red = value.R;
				OnPropertyChanged("Red");
				green = value.G;
				OnPropertyChanged("Green");
				blue = value.B;
				OnPropertyChanged("Blue");
				OnPropertyChanged("Color");
				FireSendCommand();
			}
		}
	}
}
