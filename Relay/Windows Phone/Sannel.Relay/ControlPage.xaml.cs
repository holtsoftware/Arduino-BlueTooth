using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Networking.Sockets;
using Sannel.Relay.ViewModels;
using Sannel.Relay.Command;

namespace Sannel.Relay
{
	public partial class ControlPage : PhoneApplicationPage
	{
		public static StreamSocket Connection = null;

		private CommandViewModel commandvm;
		private Processer processer;

		
		public ControlPage()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			if (Connection == null)
			{
				if (NavigationService.CanGoBack)
				{
					NavigationService.GoBack();
				}
				else
				{
					NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
					
				}
				return;
			}

			processer = new Processer();
			commandvm = new CommandViewModel();
			commandvm.Relay = new RelayViewModel();
			processer.CommandRecived += commandvm.Relay.OnCommandRecived;
			commandvm.RGBLed = new RGBLedViewModel();
			processer.CommandRecived += commandvm.RGBLed.OnCommandRecived;
			commandvm.Relay.SendCommand += sendCommand;
			commandvm.RGBLed.SendCommand += sendCommand;
			DataContext = commandvm;

			/*processer = new CommandProcesser(Connection);
			vm = new ControlViewModel();
			DataContext = vm;
			vm.Relay = new RelayViewModel();
			processer.AddCommandClass(vm.Relay);
			vm.LED = new RGBLedViewModel();
			processer.AddCommandClass(vm.LED);
			processer.SendGetAll();*/

		}

		private async void sendCommand(object sender, CommandArgs args)
		{
			if (processer != null)
			{
				await processer.SendCommandAsync(args);
			}
		}
	}
}