/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnKickOut : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92b";

		public static UI_btnKickOut CreateInstance()
		{
			return (UI_btnKickOut)UIPackage.CreateObject("UI_Guild","btnKickOut");
		}

		public UI_btnKickOut()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}