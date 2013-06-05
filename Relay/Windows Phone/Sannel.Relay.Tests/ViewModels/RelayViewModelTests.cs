using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sannel.Relay.ViewModels;
using Sannel.Relay.Command;
using System.ComponentModel;

namespace Sannel.Relay.Tests.ViewModels
{
	[TestClass]
	public class RelayViewModelTests
	{
		[TestMethod]
		public void Relay1Test()
		{
			bool sendCommandCalled = false;
			bool propertyChangedCalled = false;
			RelayViewModel relay = new RelayViewModel();
			relay.SendCommand += (sender, args) =>
				{
					sendCommandCalled = true;
					Assert.AreEqual(CommandType.Set, args.Type, "args.Type");
					Assert.AreEqual(Command.Command.Relay, args.Command, "args.Command");
					Assert.IsNotNull(args.Value, "args.Value");
					Assert.AreEqual(1, args.Value.Length, "args.Value.Length");
					Assert.AreEqual(17, args.Value[0], "args.Value[0]");
				};
			relay.PropertyChanged += (sender, args) => // Ensure that I'm sending the correct name of the property
				{
					propertyChangedCalled = true;
					Assert.AreEqual("Relay1", args.PropertyName, "args.PropertyName");
				};
			Assert.IsFalse(sendCommandCalled, "called");
			relay.Relay1 = true;
			Assert.IsTrue(relay.Relay1, "relay.Relay1");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.IsTrue(propertyChangedCalled, "propertyChangedCalled");
		}

		[TestMethod]
		public void Relay2Test()
		{
			bool sendCommandCalled = false;
			bool propertyChangedCalled = false;
			RelayViewModel relay = new RelayViewModel();
			relay.SendCommand += (sender, args) =>
			{
				sendCommandCalled = true;
				Assert.AreEqual(CommandType.Set, args.Type, "args.Type");
				Assert.AreEqual(Command.Command.Relay, args.Command, "args.Command");
				Assert.IsNotNull(args.Value, "args.Value");
				Assert.AreEqual(1, args.Value.Length, "args.Value.Length");
				Assert.AreEqual(33, args.Value[0], "args.Value[0]");
			};
			relay.PropertyChanged += (sender, args) => // Ensure that I'm sending the correct name of the property
			{
				propertyChangedCalled = true;
				Assert.AreEqual("Relay2", args.PropertyName, "args.PropertyName");
			};
			Assert.IsFalse(sendCommandCalled, "called");
			relay.Relay2 = true;
			Assert.IsTrue(relay.Relay2, "relay.Relay2");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.IsTrue(propertyChangedCalled, "propertyChangedCalled");
		}


		[TestMethod]
		public void Relay3Test()
		{
			bool sendCommandCalled = false;
			bool propertyChangedCalled = false;
			RelayViewModel relay = new RelayViewModel();
			relay.SendCommand += (sender, args) =>
			{
				sendCommandCalled = true;
				Assert.AreEqual(CommandType.Set, args.Type, "args.Type");
				Assert.AreEqual(Command.Command.Relay, args.Command, "args.Command");
				Assert.IsNotNull(args.Value, "args.Value");
				Assert.AreEqual(1, args.Value.Length, "args.Value.Length");
				Assert.AreEqual(49, args.Value[0], "args.Value[0]");
			};
			relay.PropertyChanged += (sender, args) => // Ensure that I'm sending the correct name of the property
			{
				propertyChangedCalled = true;
				Assert.AreEqual("Relay3", args.PropertyName, "args.PropertyName");
			};
			Assert.IsFalse(sendCommandCalled, "called");
			relay.Relay3 = true;
			Assert.IsTrue(relay.Relay3, "relay.Relay2");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.IsTrue(propertyChangedCalled, "propertyChangedCalled");
		}


		[TestMethod]
		public void Relay4Test()
		{
			bool sendCommandCalled = false;
			bool propertyChangedCalled = false;
			RelayViewModel relay = new RelayViewModel();
			relay.SendCommand += (sender, args) =>
			{
				sendCommandCalled = true;
				Assert.AreEqual(CommandType.Set, args.Type, "args.Type");
				Assert.AreEqual(Command.Command.Relay, args.Command, "args.Command");
				Assert.IsNotNull(args.Value, "args.Value");
				Assert.AreEqual(1, args.Value.Length, "args.Value.Length");
				Assert.AreEqual(65, args.Value[0], "args.Value[0]");
			};
			relay.PropertyChanged += (sender, args) => // Ensure that I'm sending the correct name of the property
			{
				propertyChangedCalled = true;
				Assert.AreEqual("Relay4", args.PropertyName, "args.PropertyName");
			};
			Assert.IsFalse(sendCommandCalled, "called");
			relay.Relay4 = true;
			Assert.IsTrue(relay.Relay4, "relay.Relay2");
			Assert.IsTrue(sendCommandCalled, "sendCommandCalled");
			Assert.IsTrue(propertyChangedCalled, "propertyChangedCalled");
		}

		[TestMethod]
		public void OnCommandRecivedTest()
		{
			// relay1
			CommandArgs args = new CommandArgs();
			args.Type = CommandType.Set;
			args.Command = Command.Command.Relay;
			args.Value = new byte[] { 17 };
			RelayViewModel relay = new RelayViewModel();
			relay.SendCommand += (sender, a) =>
				{
					Assert.Fail("This method should not be called");
				};
			bool called = false;
			PropertyChangedEventHandler relay1 = (sender, a) =>
				{
					called = true;
					Assert.AreEqual("Relay1", a.PropertyName, "PropertyName");
				};
			relay.PropertyChanged += relay1;
			relay.OnCommandRecived(this, args);
			Assert.IsTrue(called, "called");
			Assert.IsTrue(relay.Relay1, "relay.Relay1");
			relay.PropertyChanged -= relay1;

			// relay2
			called = false;
			PropertyChangedEventHandler relay2 = (sender, a) =>
				{
					called = true;
					Assert.AreEqual("Relay2", a.PropertyName, "PropertyName");
				};
			args.Value[0] = 33;
			relay.PropertyChanged += relay2;
			Assert.IsFalse(called, "called");
			relay.OnCommandRecived(this, args);
			Assert.IsTrue(called, "called");
			relay.PropertyChanged -= relay2;

			//relay3
			called = false;
			PropertyChangedEventHandler relay3 = (sender, a) =>
				{
					called = true;
					Assert.AreEqual("Relay3", a.PropertyName, "PropertyName");
				};
			args.Value[0] = 49;
			relay.PropertyChanged += relay3;
			Assert.IsFalse(called, "called");
			relay.OnCommandRecived(this, args);
			Assert.IsTrue(called, "called");
			relay.PropertyChanged -= relay3;

			//relay4
			called = false;
			PropertyChangedEventHandler relay4 = (sender, a) =>
				{
					called = true;
					Assert.AreEqual("Relay4", a.PropertyName, "PropertyName");
				};
			args.Value[0] = 65;
			relay.PropertyChanged += relay4;
			Assert.IsFalse(called, "called");
			relay.OnCommandRecived(this, args);
			Assert.IsTrue(called, "called");
			relay.PropertyChanged -= relay4;

		}
	}
}
