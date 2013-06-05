using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sannel.Relay.ViewModels;
using Sannel.Relay.Command;
using System.ComponentModel;
using Windows.UI;

namespace Sannel.Relay.Tests.ViewModels
{
	[TestClass]
	public class RGBLedViewModelTests
	{
		[TestMethod]
		public void RedTest()
		{
			byte redValue = 243;
			bool propertyChangedCalled = false;
			RGBLedViewModel led = new RGBLedViewModel();
			led.PropertyChanged += (sender, a) =>
				{
					propertyChangedCalled = true;
					Assert.AreEqual("Red", a.PropertyName, "a.PropertyName");
				};
			bool sendCommandCalled = false;
			led.SendCommand += (sender, a) =>
				{
					sendCommandCalled = true;
					Assert.AreEqual(CommandType.Set, a.Type, "a.Type");
					Assert.AreEqual(Command.Command.RGBLed, a.Command, "a.Command");
					Assert.AreEqual(3, a.Value.Length, "a.Value.Length");
					Assert.AreEqual(redValue, a.Value[0], "a.Value[0]");
					Assert.AreEqual(0, a.Value[1], "a.Value[1]");
					Assert.AreEqual(0, a.Value[2], "a.Value[2]");
				};
			led.Red = redValue;
			Assert.IsTrue(propertyChangedCalled, "propertyChangedCalled");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.AreEqual(redValue, led.Red, "led.Red");
		}

		[TestMethod]
		public void GreenTest()
		{
			byte greenValue = 243;
			bool propertyChangedCalled = false;
			RGBLedViewModel led = new RGBLedViewModel();
			led.PropertyChanged += (sender, a) =>
			{
				propertyChangedCalled = true;
				Assert.AreEqual("Green", a.PropertyName, "a.PropertyName");
			};
			bool sendCommandCalled = false;
			led.SendCommand += (sender, a) =>
			{
				sendCommandCalled = true;
				Assert.AreEqual(CommandType.Set, a.Type, "a.Type");
				Assert.AreEqual(Command.Command.RGBLed, a.Command, "a.Command");
				Assert.AreEqual(3, a.Value.Length, "a.Value.Length");
				Assert.AreEqual(0, a.Value[0], "a.Value[0]");
				Assert.AreEqual(greenValue, a.Value[1], "a.Value[1]");
				Assert.AreEqual(0, a.Value[2], "a.Value[2]");
			};
			led.Green = greenValue;
			Assert.IsTrue(propertyChangedCalled, "propertyChangedCalled");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.AreEqual(greenValue, led.Green, "led.Green");
		}

		[TestMethod]
		public void BlueTest()
		{
			byte blueValue = 243;
			bool propertyChangedCalled = false;
			RGBLedViewModel led = new RGBLedViewModel();
			led.PropertyChanged += (sender, a) =>
			{
				propertyChangedCalled = true;
				Assert.AreEqual("Blue", a.PropertyName, "a.PropertyName");
			};
			bool sendCommandCalled = false;
			led.SendCommand += (sender, a) =>
			{
				sendCommandCalled = true;
				Assert.AreEqual(CommandType.Set, a.Type, "a.Type");
				Assert.AreEqual(Command.Command.RGBLed, a.Command, "a.Command");
				Assert.AreEqual(3, a.Value.Length, "a.Value.Length");
				Assert.AreEqual(0, a.Value[0], "a.Value[0]");
				Assert.AreEqual(0, a.Value[1], "a.Value[1]");
				Assert.AreEqual(blueValue, a.Value[2], "a.Value[2]");
			};
			led.Blue = blueValue;
			Assert.IsTrue(propertyChangedCalled, "propertyChangedCalled");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.AreEqual(blueValue, led.Blue, "led.Blue");
		}

		[TestMethod]
		public void OnCommandRecivedTest()
		{
			CommandArgs args = new CommandArgs();
			args.Type = CommandType.Set;
			args.Command = Command.Command.RGBLed;
			args.Value = new byte[] { 0, 0, 0 };

			RGBLedViewModel led = new RGBLedViewModel();
			led.SendCommand += (sender, a) => { Assert.Fail("SendCommand should not be called"); };
			int calledTimes = 0;
			bool redName = false;
			bool greenName = false;
			bool blueName = false;
			// Red
			led.PropertyChanged += (sender, a) =>
				{
					calledTimes++;
					if(a.PropertyName == "Red")
					{
						redName = true;
					}
					else if(a.PropertyName == "Green")
					{
						greenName = true;
					}
					else if (a.PropertyName == "Blue")
					{
						blueName = true;
					}
					else
					{
						Assert.Fail("{0} is not an expected PropertyName", a.PropertyName);
					}
				};
			args.Value[0] = 123;
			args.Value[1] = 33;
			args.Value[2] = 89;

			led.OnCommandRecived(this, args);
			Assert.AreEqual(3, calledTimes, "calledTimes");
			Assert.IsTrue(redName, "redName");
			Assert.IsTrue(greenName, "greenName");
			Assert.IsTrue(blueName, "blueName");
		}

		[TestMethod]
		public void ColorTest()
		{
			var color = Color.FromArgb(255, 24, 33, 126);
			RGBLedViewModel led = new RGBLedViewModel();
			bool sendCommandCalled = false;
			led.SendCommand += (sender, a) => 
			{
				sendCommandCalled = true;
				Assert.AreEqual(CommandType.Set, a.Type, "a.Type");
				Assert.AreEqual(Command.Command.RGBLed, a.Command, "a.Command");
				Assert.IsNotNull(a.Value);
				Assert.AreEqual(3, a.Value.Length, "a.Value.Length");
				Assert.AreEqual(color.R, a.Value[0], "a.Value[0]");
				Assert.AreEqual(color.G, a.Value[1], "a.Value[1]");
				Assert.AreEqual(color.B, a.Value[2], "a.Value[2]");
			};
			int calledTimes = 0;
			bool redName = false;
			bool greenName = false;
			bool blueName = false;
			bool colorName = false;
			// Red
			led.PropertyChanged += (sender, a) =>
			{
				calledTimes++;
				if (a.PropertyName == "Red")
				{
					redName = true;
				}
				else if (a.PropertyName == "Green")
				{
					greenName = true;
				}
				else if (a.PropertyName == "Blue")
				{
					blueName = true;
				}
				else if (a.PropertyName == "Color")
				{
					colorName = true;
				}
				else
				{
					Assert.Fail("{0} is not an expected PropertyName", a.PropertyName);
				}
			};
			led.Color = color;
			Assert.AreEqual(4, calledTimes, "calledTimes");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.IsTrue(redName, "redName");
			Assert.IsTrue(greenName, "greenName");
			Assert.IsTrue(blueName, "blueName");
			Assert.IsTrue(colorName, "colorName");
			Assert.AreEqual(color.R, led.Red);
			Assert.AreEqual(color.G, led.Green);
			Assert.AreEqual(color.B, led.Blue);
		}
	}
}
