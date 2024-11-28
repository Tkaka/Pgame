/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_StarList : GComponent
	{
		public GList m_starList;

		public const string URL = "ui://42sthz3tw548jb";

		public static UI_StarList CreateInstance()
		{
			return (UI_StarList)UIPackage.CreateObject("UI_Common","StarList");
		}

		public UI_StarList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_starList = (GList)this.GetChildAt(0);
		}
	}
}