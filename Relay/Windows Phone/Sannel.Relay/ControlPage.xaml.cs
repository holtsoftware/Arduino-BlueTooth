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
using System.IO;
using Windows.Phone.Speech.Recognition;
using System.Windows.Media;

namespace Sannel.Relay
{
	public partial class ControlPage : PhoneApplicationPage
	{
		public static StreamSocket Connection = null;
		private SpeechRecognizerUI speachUI = new SpeechRecognizerUI();
		private CommandViewModel commandvm;
		private Processer processer;
		private List<ColorViewModel> colors = new List<ColorViewModel>();

		public ControlPage()
		{
			InitializeComponent();
			speachUI.Settings.ExampleText = "On/Off";
			speachUI.Settings.ListenText = "On or Off?";
			speachUI.Recognizer.Grammars.AddGrammarFromList("lights", new String[] { "on", "off" });
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
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

			if (processer == null || commandvm == null || commandvm.RGBLed == null)
			{
				processer = new Processer();
				commandvm = new CommandViewModel();
				commandvm.Relay = new RelayViewModel();
				processer.CommandRecived += commandvm.Relay.OnCommandRecived;
				commandvm.RGBLed = new RGBLedViewModel();
				processer.CommandRecived += commandvm.RGBLed.OnCommandRecived;
				commandvm.Relay.SendCommand += sendCommand;
				commandvm.RGBLed.SendCommand += sendCommand;
				DataContext = commandvm;
				processer.SetInputStream(Connection.InputStream.AsStreamForRead());
				processer.SetOutputStream(Connection.OutputStream.AsStreamForWrite());

				if (NavigationContext.QueryString != null && NavigationContext.QueryString.ContainsKey("commandName"))
				{
					if (String.Compare("LightsOn", NavigationContext.QueryString["commandName"], StringComparison.CurrentCulture) == 0)
					{
						commandvm.Relay.Relay1 = commandvm.Relay.Relay2 = commandvm.Relay.Relay3 = commandvm.Relay.Relay4 = true;
					}
					else if (String.Compare("LightsOff", NavigationContext.QueryString["commandName"], StringComparison.CurrentCulture) == 0)
					{
						commandvm.Relay.Relay1 = commandvm.Relay.Relay2 = commandvm.Relay.Relay3 = commandvm.Relay.Relay4 = false;
					}
				}

				CommandArgs args = new CommandArgs();
				args.Type = CommandType.Get;
				args.Command = Command.Command.All;
				args.Value = new byte[0];
				
				colors.AddRange(new ColorViewModel[]
				{
					new ColorViewModel
					{
						Name = "None",
						Color = Colors.Black
					},
					new ColorViewModel()
					{
						Name = "Blue",
						Color = Colors.Blue
					},
					new ColorViewModel
					{
						Name = "Brown",
						Color = Colors.Brown
					},
					new ColorViewModel{
						Name = "Cyan",
						Color = Colors.Cyan
					},
					new ColorViewModel
					{
						Name = "DarkGray",
						Color = Colors.DarkGray
					},
					new ColorViewModel
					{
						Name = "Gray",
						Color = Colors.Gray
					},
					new ColorViewModel
					{
						Name = "Green",
						Color = Colors.Green
					},
					new ColorViewModel
					{
						Name = "LightGray",
						Color = Colors.LightGray
					},
					new ColorViewModel
					{
						Name = "Magenta",
						Color = Colors.Magenta
					},
					new ColorViewModel
					{
						Name = "Orange",
						Color = Colors.Orange
					},
					new ColorViewModel
					{
						Name = "Purple",
						Color = Colors.Purple
					},
					new ColorViewModel
					{
						Name = "Red",
						Color = Colors.Red
					},
					new ColorViewModel
					{
						Name = "White",
						Color = Colors.White
					},
					new ColorViewModel
					{
						Name = "Yellow",
						Color = Colors.Yellow
					}
				});
				ColorsInput.ItemsSource = colors;

				if (processer != null)
				{
					await processer.SendCommandAsync(args);
				}
			}
		}

		private async void sendCommand(object sender, CommandArgs args)
		{
			if (processer != null)
			{
				await processer.SendCommandAsync(args);
			}
		}

		protected async void Speech_Click(object sender, EventArgs e)
		{
			var results = await speachUI.RecognizeWithUIAsync();
			if (results.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
			{
				if (String.Compare(results.RecognitionResult.Text, "on") == 0)
				{
					commandvm.Relay.Relay1 = commandvm.Relay.Relay2 = commandvm.Relay.Relay3 = commandvm.Relay.Relay4 = true;
				}
				else if (String.Compare(results.RecognitionResult.Text, "off") == 0)
				{
					commandvm.Relay.Relay1 = commandvm.Relay.Relay2 = commandvm.Relay.Relay3 = commandvm.Relay.Relay4 = false;
				}
			}
		}

		private void ColorsInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				ColorViewModel color = e.AddedItems[0] as ColorViewModel;
				commandvm.RGBLed.Red = color.Color.R;
				commandvm.RGBLed.Green = color.Color.G;
				commandvm.RGBLed.Blue = color.Color.B;
			}
		}
	}
}