/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_GuildHallWnd : GComponent
	{
		public Controller m_c1;
		public UI_windowCloseBtn m_btnClose;
		public GList m_tabList;

		public const string URL = "ui://oe7ras64qbwu6";

		public static UI_GuildHallWnd CreateInstance()
		{
			return (UI_GuildHallWnd)UIPackage.CreateObject("UI_Guild","GuildHallWnd");
		}

		public UI_GuildHallWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(1);
			m_tabList = (GList)this.GetChildAt(2);
		}
	}
}