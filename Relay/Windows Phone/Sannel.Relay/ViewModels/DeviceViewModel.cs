using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace Sannel.Relay.ViewModels
{
	public class DeviceViewModel : Sannel.ComponentModel.NotifyPropertyChangedBase
	{
		private String name;
		public String Name
		{
			get
			{
				return name;
			}
			set
			{
				SetProperty(ref name, value, "Name");
			}
		}

		private String address;
		public String Address
		{
			get
			{
				return address;
			}
			set
			{
				SetProperty(ref address, value, "Address");
			}
		}

		private String serviceName;
		public String ServiceName
		{
			get
			{
				return serviceName;
			}
			set
			{
				SetProperty(ref serviceName, value, "ServiceName");
			}
		}

		private HostName hostName;
		public HostName HostName
		{
			get
			{
				return hostName;
			}
			set
			{
				SetProperty(ref hostName, value, "HostName");
			}
		}

	}
}
