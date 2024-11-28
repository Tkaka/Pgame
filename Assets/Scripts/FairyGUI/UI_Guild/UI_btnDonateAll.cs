/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnDonateAll : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64lcbob45";

		public static UI_btnDonateAll CreateInstance()
		{
			return (UI_btnDonateAll)UIPackage.CreateObject("UI_Guild","btnDonateAll");
		}

		public UI_btnDonateAll()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}