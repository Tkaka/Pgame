/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnShenZhi : GComponent
	{
		public GImage m_btnShenZhi;
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64105rb3d";

		public static UI_btnShenZhi CreateInstance()
		{
			return (UI_btnShenZhi)UIPackage.CreateObject("UI_Guild","btnShenZhi");
		}

		public UI_btnShenZhi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnShenZhi = (GImage)this.GetChildAt(0);
			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}