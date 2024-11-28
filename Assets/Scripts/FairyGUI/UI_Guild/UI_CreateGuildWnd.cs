/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_CreateGuildWnd : GComponent
	{
		public Controller m_c1;
		public GTextField m_txtCondition;
		public GTextInput m_txtGuildName;
		public UI_objBadge m_objBadge;
		public GTextField m_txtDiamiondNum;
		public UI_btnCreateGuild m_btnCreate;

		public const string URL = "ui://oe7ras64f1jg32";

		public static UI_CreateGuildWnd CreateInstance()
		{
			return (UI_CreateGuildWnd)UIPackage.CreateObject("UI_Guild","CreateGuildWnd");
		}

		public UI_CreateGuildWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
			m_txtCondition = (GTextField)this.GetChildAt(5);
			m_txtGuildName = (GTextInput)this.GetChildAt(7);
			m_objBadge = (UI_objBadge)this.GetChildAt(8);
			m_txtDiamiondNum = (GTextField)this.GetChildAt(12);
			m_btnCreate = (UI_btnCreateGuild)this.GetChildAt(13);
		}
	}
}