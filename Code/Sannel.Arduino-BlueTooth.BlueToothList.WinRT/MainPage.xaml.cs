using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Sannel.Arduino_BlueTooth.BlueToothList.WinRT
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.  The Parameter
		/// property is typically used to configure the page.</param>
		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{


			StreamSocket socket = new StreamSocket();
			await socket.ConnectAsync(new HostName("0006664FB88A"), "{00001101-0000-1000-8000-00805f9b34fb}");
			using (var stream = new StreamWriter(socket.OutputStream.AsStreamForWrite()))
			{
				await stream.WriteLineAsync("Connectecd");
			}

			/*PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";
			PeerFinder.AllowBluetooth = true;
			PeerFinder.AllowInfrastructure = false;
			//PeerFinder.AllowWiFiDirect = false;
			PeerFinder.Start();
			var available_devices = await PeerFinder.FindAllPeersAsync();

			MainLongListSelector.ItemsSource = available_devices;*/
		}
	}
}
