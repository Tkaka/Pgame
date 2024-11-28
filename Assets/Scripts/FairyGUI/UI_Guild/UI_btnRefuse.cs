/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnRefuse : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92g";

		public static UI_btnRefuse CreateInstance()
		{
			return (UI_btnRefuse)UIPackage.CreateObject("UI_Guild","btnRefuse");
		}

		public UI_btnRefuse()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}