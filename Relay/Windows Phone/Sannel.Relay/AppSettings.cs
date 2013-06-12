using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sannel.Relay
{
	public class AppSettings
	{
		private static AppSettings settings;
		IsolatedStorageSettings localSettings;
		private const String DEFAULTADDRESS = "DefaultAddress";

		public AppSettings()
		{
			localSettings = IsolatedStorageSettings.ApplicationSettings;
		}

		public static AppSettings Current
		{
			get
			{
				return settings ?? (settings = new AppSettings());
			}
		}

		private T getValueOrDefault<T>(String key, T defaultValue)
		{
			if (localSettings.Contains(key))
			{
				if (localSettings[key] is T)
				{
					return (T)localSettings[key];
				}
			}

			return defaultValue;
		}

		private void setOrUpdate<T>(String key, T value)
		{
			localSettings[key] = value;
			localSettings.Save();
		}

		public String DefaultAddress
		{
			get
			{
				return getValueOrDefault<String>(DEFAULTADDRESS, null);
			}
			set
			{
				setOrUpdate(DEFAULTADDRESS, value);
			}
		}
	}
}
