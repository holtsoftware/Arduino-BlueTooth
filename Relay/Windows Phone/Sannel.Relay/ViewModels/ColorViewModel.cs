using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sannel.Relay.ViewModels
{
	public class ColorViewModel : Sannel.ComponentModel.NotifyPropertyChangedBase
	{
		private String name;
		public String Name
		{
			get
			{
				return name;
			}
			set
			{
				SetProperty(ref name, value, "Name");
			}
		}

		private Color color;
		public Color Color
		{
			get
			{
				return color;
			}
			set
			{
				SetProperty(ref color, value, "Color");
			}
		}
	}
}
