using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sannel.Relay.ViewModels;
using Sannel.Relay.Command;

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
	}
}
