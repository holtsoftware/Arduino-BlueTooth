using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using System.IO;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace Sannel.Arduino_BlueTooth.BlueToothList
{
	public partial class LED : PhoneApplicationPage
	{
		private Stream stream;
		private StreamReader reader;
		private Object lockObject = new Object();
		private bool preventSend = false;

		public LED()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			if (App.ConnectedSocket == null)
			{
				NavigationService.GoBack();
				return;
			}

			stream = App.ConnectedSocket.OutputStream.AsStreamForWrite();
			reader = new StreamReader(App.ConnectedSocket.InputStream.AsStreamForRead(0));
			stream.Write(new byte[]
			{
				2,
				0,
				0,
				0
			}, 0, 4);
			stream.Flush();
			BeginRead();
		}

		private async void BeginRead()
		{
			await Task.Run(() =>
				{
					while (reader != null)
					{
						String line = reader.ReadLine();
						if (!String.IsNullOrWhiteSpace(line))
						{
							String[] commands = line.Split(',');
							if (commands.Length == 3)
							{
								if (commands[0] == "LED")
								{
									int value = 0;
									if (commands[1] == "0")
									{
										if (int.TryParse(commands[2], out value))
										{
											lock (lockObject)
											{
												preventSend = true;
												Dispatcher.BeginInvoke(() =>
												{
													RedSlider.Value = value;
												});
												preventSend = false;
											}
										}
									}
									else if (commands[1] == "1")
									{
										if (int.TryParse(commands[2], out value))
										{
											lock (lockObject)
											{
												preventSend = true;
												Dispatcher.BeginInvoke(() =>
													{
														GreenSlider.Value = value;
													});
												preventSend = false;
											}
										}
									}
									else if (commands[1] == "2")
									{
										if (int.TryParse(commands[2], out value))
										{
											lock (lockObject)
											{
												preventSend = true;
												Dispatcher.BeginInvoke(() =>
												{
													BlueSlider.Value = value;
												});
												preventSend = false;
											}
										}
									}
								}
								else if (commands[0] == "RELAY")
								{
									switch (commands[1])
									{
										case "0":
											lock (lockObject)
											{
												preventSend = true;
												Dispatcher.BeginInvoke(() =>
												{
													Relay1Action.IsChecked = commands[2] == "1";
												});
												preventSend = false;
											}
											break;

										case "1":
											lock (lockObject)
											{
												preventSend = true;
												Dispatcher.BeginInvoke(() =>
												{
													Relay2Action.IsChecked = commands[2] == "1";
												});
												preventSend = false;
											}
											break;

										case "2":
											lock (lockObject)
											{
												preventSend = true;
												Dispatcher.BeginInvoke(() =>
												{
													Relay3Action.IsChecked = commands[2] == "1";
												});
												preventSend = false;
											}
											break;

										case "3":
											lock (lockObject)
											{
												preventSend = true;
												Dispatcher.BeginInvoke(() =>
													{
														Relay4Action.IsChecked = commands[2] == "1";
													});
												preventSend = false;
											}
											break;
									}
								}
							}
						}
					}
				});
		}

		protected async void SetLedLine(int id, double value)
		{
			await stream.WriteAsync(new byte[]
				{
					1,
					1,
					(byte)id,
					(byte)value
				}, 0, 4);
		}

		protected async void SetServo(int id, double value)
		{
			await stream.WriteAsync(new byte[]
				{
					1,
					2,
					(byte)id,
					(byte)value
				}, 0, 4);
		}

		protected async Task SendCommand(int type, int id, double value)
		{
			if (stream != null)
			{
				await Task.Run(() =>
					{
						lock (lockObject)
						{
							if (!preventSend)
							{
								stream.Write(new byte[] 
								{
									1,
									(byte)type,
									(byte)id,
									(byte)value
								}, 0, 4);
								stream.Flush();
							}
						}
					});
			}
		}

		private async void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			await SendCommand(1, 0, RedSlider.Value);
		}

		private async void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			await SendCommand(1, 1, e.NewValue);
		}

		private async void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			await SendCommand(1, 2, e.NewValue);
		}

		private async void ServoSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			await SendCommand(2, 1, e.NewValue);
		}

		private async void Relay1Action_Checked(object sender, RoutedEventArgs e)
		{
			if (Relay1Action.IsChecked.Value)
			{
				await SendCommand(3, 0, 1);
			}
			else
			{
				await SendCommand(3, 0, 0);
			}
		}

		private async void Relay2Action_Checked(object sender, RoutedEventArgs e)
		{
			if (Relay2Action.IsChecked.Value)
			{
				await SendCommand(3, 1, 1);
			}
			else
			{
				await SendCommand(3, 1, 0);
			}
		}

		private async void Relay3Action_Click(object sender, RoutedEventArgs e)
		{
			if (Relay3Action.IsChecked.Value)
			{
				await SendCommand(3, 2, 1);
			}
			else
			{
				await SendCommand(3, 2, 0);
			}
		}

		private async void Relay4Action_Click(object sender, RoutedEventArgs e)
		{
			if (Relay4Action.IsChecked.Value)
			{
				await SendCommand(3, 3, 1);
			}
			else
			{
				await SendCommand(3, 3, 0);
			}
		}
	}
}