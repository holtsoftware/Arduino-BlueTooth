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

namespace Sannel.Relay
{
	public partial class ControlPage : PhoneApplicationPage
	{
		public static StreamSocket Connection = null;

		
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

			/*processer = new CommandProcesser(Connection);
			vm = new ControlViewModel();
			DataContext = vm;
			vm.Relay = new RelayViewModel();
			processer.AddCommandClass(vm.Relay);
			vm.LED = new RGBLedViewModel();
			processer.AddCommandClass(vm.LED);
			processer.SendGetAll();*/

		}
	}
}