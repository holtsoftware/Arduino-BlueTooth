using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.Command.Tests
{
	[TestClass]
	public class ProcesserTests
	{
		private byte[] generatePacket(CommandArgs args)
		{
			List<byte> packet = new List<byte>();
			packet.Add(Processer.StartByte);
			int crc = 255;
			int length = 10 + args.Value.Length;
			packet.Add((byte)length);
			crc = crc ^ packet[1];
			packet.Add((byte)((uint)args.Type >> 24));
			crc = crc ^ packet[2];
			packet.Add((byte)((uint)args.Type >> 16));
			crc = crc ^ packet[3];
			packet.Add((byte)((uint)args.Type >> 8));
			crc = crc ^ packet[4];
			packet.Add((byte)args.Type);
			crc = crc ^ packet[5];
			packet.Add((byte)((uint)args.Command >> 24));
			crc = crc ^ packet[6];
			packet.Add((byte)((uint)args.Command >> 16));
			crc = crc ^ packet[7];
			packet.Add((byte)((uint)args.Command >> 8));
			crc = crc ^ packet[8];
			packet.Add((byte)args.Command);
			crc = crc ^ packet[9];
			int index = 0;

			for (int i = 0; i < args.Value.Length; i++)
			{
				packet.Add(args.Value[i]);
				index = packet.Count - 1;
				crc = crc ^ packet[index];
			}

			packet.Add((byte)crc);
			packet.Add(Processer.EndByte);

			return packet.ToArray();
		}

		/*
		 * Package
		 * [1 byte (start) (254)][1 byte (Length in bytes max 253)][4 bytes (CommandType)][4 bytes (Command)][0-243 bytes (Data)][1 byte (CRC)][1 byte (stop) (255)]
		 */
		[TestMethod]
		public void GeneratePackageTest()
		{
			CommandArgs args = new CommandArgs();
			args.Type = CommandType.Get;
			args.Command = Command.Relay;
			args.Value = new byte[] { (1 << 4) | 1 };

			var expected = generatePacket(args);

			ProcesserExposer processer = new ProcesserExposer();
			var actual = processer.GeneratePackageExposed(args);
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.Length, actual.Length, "actual.Length");
			for (int i = 0; i < actual.Length; i++)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}

		[TestMethod]
		public void ReadPacketTest()
		{
			CommandArgs expected = new CommandArgs();
			expected.Type = CommandType.Set;
			expected.Command = Command.RGBLed;
			expected.Value = new byte[] { 255, 255, 255 };

			var data = generatePacket(expected);

			MemoryStream stream = new MemoryStream(data);
			stream.ReadByte(); // Skip the start byte as ReadPacket assumes that you have just read it.
			ProcesserExposer processer = new ProcesserExposer();
			var actual = processer.ReadPacketExposed(stream);
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.Type, actual.Type, "actual.Type");
			Assert.AreEqual(expected.Command, actual.Command, "actual.Command");
			Assert.IsNotNull(actual.Value, "actual.Value");
			Assert.AreEqual(expected.Value.Length, actual.Value.Length, "actual.Value.Length");
			for (int i = 0; i < actual.Value.Length; i++)
			{
				Assert.AreEqual(expected.Value[i], actual.Value[i], "actual.Value[i]");
			}
		}

		[TestMethod]
		public async Task SendCommandAsyncTest()
		{
			CommandArgs args = new CommandArgs();
			args.Type = CommandType.Set;
			args.Command = Command.Relay;
			args.Value = new byte[] { 243, 221, 255 };

			var expected = generatePacket(args);
			Processer processer = new Processer();
			MemoryStream outstream = new MemoryStream();
			processer.SetOutputStream(outstream);
			await processer.SendCommandAsync(args);

			var actual = outstream.ToArray();

			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.Length, actual.Length, "actual.Length");
			for (int i = 0; i < actual.Length; i++)
			{
				Assert.AreEqual(expected[i], actual[i], "actual[i]");
			}
		}

		[TestMethod]
		public async Task CommandRecivedTest()
		{
			bool called = false;
			CommandArgs expected = new CommandArgs();
			expected.Type = CommandType.Get;
			expected.Command = Command.RGBLed;
			expected.Value = new byte[] { 122, 30, 112 };

			var bytes = generatePacket(expected);
			MemoryStream stream = new MemoryStream(bytes);
			stream.Seek(0, SeekOrigin.Begin);
			Processer processer = new Processer();
			processer.CommandRecived += (sender, actual) =>
				{
					called = true;
					Assert.AreEqual(expected.Type, actual.Type, "actual.Type");
					Assert.AreEqual(expected.Command, actual.Command, "actual.Command");
					Assert.AreEqual(expected.Value.Length, actual.Value.Length, "actual.Value.Length");
					for (int i = 0; i < actual.Value.Length; i++)
					{
						Assert.AreEqual(expected.Value[i], actual.Value[i], "actual.Value[i]");
					}
				};
			processer.SetInputStream(stream);
			await Task.Delay(500); // Delay to make sure the data has been read in by the processInput Task
			Assert.IsTrue(called, "called");
		}
	}
}
