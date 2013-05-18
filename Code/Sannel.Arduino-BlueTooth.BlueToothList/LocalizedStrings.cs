using Sannel.Arduino_BlueTooth.BlueToothList.Resources;

namespace Sannel.Arduino_BlueTooth.BlueToothList
{
	/// <summary>
	/// Provides access to string resources.
	/// </summary>
	public class LocalizedStrings
	{
		private static AppResources _localizedResources = new AppResources();

		public AppResources LocalizedResources { get { return _localizedResources; } }
	}
}