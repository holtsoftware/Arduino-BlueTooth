using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.ViewModels
{
	public class DevicesViewModel : Sannel.ComponentModel.NotifyPropertyChangedBase
	{
		public ObservableCollection<DeviceViewModel> Devices { get; private set; }

		public DevicesViewModel()
		{
			Devices = new ObservableCollection<DeviceViewModel>();
		}
	}
}
