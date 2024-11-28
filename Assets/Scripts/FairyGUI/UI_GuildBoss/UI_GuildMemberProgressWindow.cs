/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildMemberProgressWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_closeBtn;
		public GList m_memberList;
		public GButton m_doubuleSelectBtn;

		public const string URL = "ui://u2d86ulcjg9f9";

		public static UI_GuildMemberProgressWindow CreateInstance()
		{
			return (UI_GuildMemberProgressWindow)UIPackage.CreateObject("UI_GuildBoss","GuildMemberProgressWindow");
		}

		public UI_GuildMemberProgressWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_closeBtn = (GButton)this.GetChildAt(5);
			m_memberList = (GList)this.GetChildAt(6);
			m_doubuleSelectBtn = (GButton)this.GetChildAt(7);
		}
	}
}