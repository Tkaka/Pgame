/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_ChatChannelSelectWnd : GComponent
	{
		public GButton m_btnClose;
		public UI_btnOk m_btnOk;
		public GButton m_btnWorld;
		public GButton m_btnTeam;
		public GButton m_btnGuild;
		public GButton m_btnSystem;
		public GButton m_btnZuDui;

		public const string URL = "ui://51gazvjdkb3122";

		public static UI_ChatChannelSelectWnd CreateInstance()
		{
			return (UI_ChatChannelSelectWnd)UIPackage.CreateObject("UI_Chat","ChatChannelSelectWnd");
		}

		public UI_ChatChannelSelectWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(2);
			m_btnOk = (UI_btnOk)this.GetChildAt(4);
			m_btnWorld = (GButton)this.GetChildAt(10);
			m_btnTeam = (GButton)this.GetChildAt(11);
			m_btnGuild = (GButton)this.GetChildAt(12);
			m_btnSystem = (GButton)this.GetChildAt(13);
			m_btnZuDui = (GButton)this.GetChildAt(14);
		}
	}
}