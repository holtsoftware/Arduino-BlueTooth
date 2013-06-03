using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.ViewModels
{
	public class CommandViewModel : Sannel.ComponentModel.NotifyPropertyChangedBase
	{
		private RelayViewModel relay;
		public RelayViewModel Relay
		{
			get
			{
				return relay;
			}
			set
			{
				SetProperty(ref relay, value, "Relay");
			}
		}

		private RGBLedViewModel rgbLed;
		public RGBLedViewModel RGBLed
		{
			get
			{
				return rgbLed;
			}
			set
			{
				SetProperty(ref rgbLed, value, "Relay");
			}
		}
	}
}
