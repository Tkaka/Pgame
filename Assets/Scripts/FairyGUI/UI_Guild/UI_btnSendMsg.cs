/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnSendMsg : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92a";

		public static UI_btnSendMsg CreateInstance()
		{
			return (UI_btnSendMsg)UIPackage.CreateObject("UI_Guild","btnSendMsg");
		}

		public UI_btnSendMsg()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}