/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_JoinGuildMainWnd : GComponent
	{
		public Controller m_tabControl;
		public UI_windowCloseBtn m_btnClose;

		public const string URL = "ui://oe7ras64f1jg2r";

		public static UI_JoinGuildMainWnd CreateInstance()
		{
			return (UI_JoinGuildMainWnd)UIPackage.CreateObject("UI_Guild","JoinGuildMainWnd");
		}

		public UI_JoinGuildMainWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tabControl = this.GetControllerAt(0);
			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(1);
		}
	}
}