using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sannel.Relay.Resources;
using Windows.Networking.Proximity;
using Sannel.Relay.ViewModels;
using System.Net.Sockets;
using Windows.Networking.Sockets;
using System.Diagnostics;
using Windows.Networking;
using System.Threading.Tasks;

namespace Sannel.Relay
{
	public partial class MainPage : PhoneApplicationPage
	{
		private static String SERVICEID = "{00001101-0000-1000-8000-00805f9b34fb}";
		private static DevicesViewModel model = new DevicesViewModel();
		// Constructor
		public MainPage()
		{
			InitializeComponent();

			// Sample code to localize the ApplicationBar
			//BuildLocalizedApplicationBar();
			DataContext = model;
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			LoadingProgressBar.Visibility = System.Windows.Visibility.Visible;

			PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";
			PeerFinder.AlternateIdentities["Bluetooth:SDP"] = SERVICEID;
			var availableDevices = await PeerFinder.FindAllPeersAsync();

			if (availableDevices != null)
			{
				model.Devices.Clear();

				foreach (var device in availableDevices)
				{
					var dvm = new DeviceViewModel();
					dvm.HostName = device.HostName;
					dvm.Name = device.DisplayName;
					dvm.Address = device.HostName.DisplayName;
					dvm.ServiceName = device.ServiceName;
					
					Dispatcher.BeginInvoke(() =>
						{
							model.Devices.Add(dvm);
						});
				}
			}

			LoadingProgressBar.Visibility = System.Windows.Visibility.Collapsed;
			String voiceCommandName = null;

			if(this.NavigationContext.QueryString != null && this.NavigationContext.QueryString.ContainsKey("voiceCommandName"))
			{
				voiceCommandName = NavigationContext.QueryString["voiceCommandName"];
			}


			if(!String.IsNullOrWhiteSpace(AppSettings.Current.DefaultAddress) && e.NavigationMode != NavigationMode.Back)
			{
				Dispatcher.BeginInvoke(() =>
				{
					ConnectingDialog.Visibility = System.Windows.Visibility.Visible;
				});

				await connectToDeviseAsync(new HostName(AppSettings.Current.DefaultAddress));

				Dispatcher.BeginInvoke(() =>
				{
					ConnectingDialog.Visibility = System.Windows.Visibility.Collapsed;
				});
				if (ControlPage.Connection != null)
				{
					if (!String.IsNullOrWhiteSpace(voiceCommandName))
					{
						NavigationService.Navigate(new Uri("/ControlPage.xaml?commandName=" + Uri.EscapeDataString(voiceCommandName), UriKind.Relative));
					}
					else
					{
						NavigationService.Navigate(new Uri("/ControlPage.xaml", UriKind.Relative));
					}
				}
			}
		}

		private async Task connectToDeviseAsync(HostName name)
		{
			if (ControlPage.Connection != null)
			{
				ControlPage.Connection.Dispose();
				ControlPage.Connection = null;
			}
			try
			{
				StreamSocket socket = new StreamSocket();

				await socket.ConnectAsync(name, SERVICEID);

				ControlPage.Connection = socket;
			}
			catch (Exception e)
			{
				MessageBox.Show("Unable to connect to device.", "Error", MessageBoxButton.OK);
			}
		}

		private async void DevicesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				var item = e.AddedItems[0] as DeviceViewModel;
				if (item != null)
				{
					Dispatcher.BeginInvoke(() =>
						{
							ConnectingDialog.Visibility = System.Windows.Visibility.Visible;
						});

					await connectToDeviseAsync(item.HostName);

					Dispatcher.BeginInvoke(() =>
						{
							ConnectingDialog.Visibility = System.Windows.Visibility.Collapsed;
						});
					if (ControlPage.Connection != null)
					{
						var results = MessageBox.Show("Make default device?", "Make Default", MessageBoxButton.OKCancel);
						if (results == MessageBoxResult.OK)
						{
							AppSettings.Current.DefaultAddress = item.HostName.RawName;
						}
						NavigationService.Navigate(new Uri("/ControlPage.xaml", UriKind.Relative));
					}

				}
			}
		}


		// Sample code for building a localized ApplicationBar
		//private void BuildLocalizedApplicationBar()
		//{
		//    // Set the page's ApplicationBar to a new instance of ApplicationBar.
		//    ApplicationBar = new ApplicationBar();

		//    // Create a new button and set the text value to the localized string from AppResources.
		//    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
		//    appBarButton.Text = AppResources.AppBarButtonText;
		//    ApplicationBar.Buttons.Add(appBarButton);

		//    // Create a new menu item with the localized string from AppResources.
		//    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
		//    ApplicationBar.MenuItems.Add(appBarMenuItem);
		//}
	}
}