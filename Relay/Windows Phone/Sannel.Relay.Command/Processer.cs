using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sannel.Relay.Command
{
	public class Processer
	{
		public static readonly byte StartByte = 254;
		public static readonly byte EndByte = 255;

		public event CommandDelegate CommandRecived;

		private Stream inputStream;
		private Stream outputStream;
		private bool stop = false;

		public Processer()
		{
			processInput();
		}

		public void Stop()
		{
			stop = true;
		}

		public void SetInputStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			inputStream = stream;
		}

		public void SetOutputStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			outputStream = stream;
		}

		public async Task SendCommandAsync(CommandArgs args)
		{
			if(outputStream != null)
			{
				var bytes = GeneratePackage(args);
				await outputStream.WriteAsync(bytes, 0, bytes.Length);
			}
		}

		private Task processInput()
		{
			return Task.Run(async () =>
				{
					int val;
					CommandArgs args = null;
					while (!stop)
					{
						if (inputStream != null)
						{
							while ((val = inputStream.ReadByte()) != -1)
							{
								if (val == StartByte)
								{
									args = ReadPacket(inputStream);
									if (args != null)
									{
										FireCommandRecived(args);
									}
								}
							}
						}
						else
						{
							await Task.Delay(250);
						}
					}
				});
		}

		protected async void FireCommandRecived(CommandArgs args)
		{
			await Task.Run(() =>
			{
				if (CommandRecived != null)
				{
					lock (CommandRecived)
					{
						CommandRecived(this, args);
					}
				}
			});
		}

		/// <summary>
		/// We assume that you have already encountered StartByte right before you call this method.
		/// </summary>
		/// <param name="input">The Stream to read the packet from </param>
		/// <returns>null if there was an issue with the packet </returns>
		protected CommandArgs ReadPacket(Stream input)
		{
			int crc = 255;
			int length = input.ReadByte();
			if (length <= -1)
			{
				return null;
			}

			crc = crc ^ length;

			byte[] buffer = new byte[4];
			buffer[0] = (byte)input.ReadByte();
			crc = crc ^ buffer[0];
			buffer[1] = (byte)input.ReadByte();
			crc = crc ^ buffer[1];
			buffer[2] = (byte)input.ReadByte();
			crc = crc ^ buffer[2];
			buffer[3] = (byte)input.ReadByte();
			crc = crc ^ buffer[3];

			int commandType = (buffer[0] << 24) | (buffer[1] << 16) | (buffer[2] << 8) | buffer[3];
			buffer[0] = (byte)input.ReadByte();
			crc = crc ^ buffer[0];
			buffer[1] = (byte)input.ReadByte();
			crc = crc ^ buffer[1];
			buffer[2] = (byte)input.ReadByte();
			crc = crc ^ buffer[2];
			buffer[3] = (byte)input.ReadByte();
			crc = crc ^ buffer[3];

			int command = (buffer[0] << 24) | (buffer[1] << 16) | (buffer[2] << 8) | (buffer[3]);

			buffer = new byte[length - 10];
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)input.ReadByte();
				crc = crc ^ buffer[i];
			}

			byte acrc = (byte)input.ReadByte();
			if (acrc != (byte)crc)
			{
				return null;
			}

			int end = input.ReadByte();
			if (end != EndByte)
			{
				return null;
			}

			CommandArgs args = new CommandArgs();

			try
			{
				args.Type = (CommandType)commandType;
			}
			catch { return null; }

			try
			{
				args.Command = (Command)command;
			}
			catch { return null; }

			args.Value = buffer;

			return args;
		}

		/*
		 * Package
		 * [1 byte (start) (254)][1 byte (Length in bytes max 253)][4 bytes (CommandType)][4 bytes (Command)][0-243 bytes (Data)][1 byte (CRC)][1 byte (stop) (255)]
		 */
		protected byte[] GeneratePackage(CommandArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}

			byte[] bytes = null;
			if (args.Value != null)
			{
				bytes = new byte[12 + args.Value.Length];
			}
			else
			{
				bytes = new byte[12];
			}

			int tmp;

			byte crc = 255;

			bytes[0] = StartByte;
			bytes[1] = (byte)(bytes.Length - 2);
			crc = (byte)(crc ^ bytes[1]);
			tmp = (int)args.Type;
			bytes[2] = (byte)(tmp >> 24);
			crc = (byte)(crc ^ bytes[2]);
			bytes[3] = (byte)(tmp >> 16);
			crc = (byte)(crc ^ bytes[3]);
			bytes[4] = (byte)(tmp >> 8);
			crc = (byte)(crc ^ bytes[4]);
			bytes[5] = (byte)tmp;
			crc = (byte)(crc ^ bytes[5]);
			tmp = (int)args.Command;
			bytes[6] = (byte)(tmp >> 24);
			crc = (byte)(crc ^ bytes[6]);
			bytes[7] = (byte)(tmp >> 16);
			crc = (byte)(crc ^ bytes[7]);
			bytes[8] = (byte)(tmp >> 8);
			crc = (byte)(crc ^ bytes[8]);
			bytes[9] = (byte)tmp;
			crc = (byte)(crc ^ bytes[9]);

			for (int i = 0; i < args.Value.Length; i++)
			{
				bytes[i + 10] = args.Value[i];
				crc = (byte)(crc ^ args.Value[i]);
			}

			bytes[10 + args.Value.Length] = crc;
			bytes[bytes.Length - 1] = EndByte;

			return bytes;
		}
	}
}
