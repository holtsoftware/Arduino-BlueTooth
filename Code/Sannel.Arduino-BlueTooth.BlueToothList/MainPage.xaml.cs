using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sannel.Arduino_BlueTooth.BlueToothList.Resources;
using Sannel.Arduino_BlueTooth.BlueToothList.ViewModels;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using System.IO;

namespace Sannel.Arduino_BlueTooth.BlueToothList
{
	public partial class MainPage : PhoneApplicationPage
	{
		// Constructor
		public MainPage()
		{
			InitializeComponent();

			// Set the data context of the LongListSelector control to the sample data
			DataContext = App.ViewModel;

			// Sample code to localize the ApplicationBar
			//BuildLocalizedApplicationBar();
		}

		// Load data for the ViewModel Items
		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			/*if (!App.ViewModel.IsDataLoaded)
			{
				App.ViewModel.LoadData();
			}*/
			ConnectingContainer.Visibility = System.Windows.Visibility.Collapsed;

			PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = ""; 
			PeerFinder.AlternateIdentities["Bluetooth:SDP"] = "{00001101-0000-1000-8000-00805f9b34fb}";
			var available_devices = await PeerFinder.FindAllPeersAsync();

			if (available_devices.Count > 0)
			{
				
				MainLongListSelector.ItemsSource = available_devices.ToList();
			}
		}

		// Handle selection changed on LongListSelector
		private async void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// If selected item is null (no selection) do nothing
			if (MainLongListSelector.SelectedItem == null)
				return;

			// Navigate to the new page
			//NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

			// Reset selected item to null (no selection)
			//MainLongListSelector.SelectedItem = null;

			var pi = MainLongListSelector.SelectedItem as PeerInformation;
			if (pi != null)
			{
				ConnectingContainer.Visibility = System.Windows.Visibility.Visible;

				if (App.ConnectedSocket != null)
				{
					App.ConnectedSocket.Dispose();
					App.ConnectedSocket = null;
				}

				StreamSocket socket = new StreamSocket();
				//socket.Control.NoDelay = false;
				//socket.Control.OutboundBufferSizeInBytes = 1;
				await socket.ConnectAsync(pi.HostName, "{00001101-0000-1000-8000-00805f9b34fb}");
				App.ConnectedSocket = socket;

				NavigationService.Navigate(new Uri("/LED.xaml", UriKind.Relative));

				/*using (StreamReader reader = new StreamReader(socket.InputStream.AsStreamForRead()))
				{
					Output.Visibility = Visibility.Visible;
					char[] buffer = new char[1];
					while (true)
					{
						await reader.ReadAsync(buffer, 0, buffer.Length);
						Output.Text += buffer[0];
					}
				}*/
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