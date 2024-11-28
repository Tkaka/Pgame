/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossRuleWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_contentList;
		public GButton m_closeBtn;

		public const string URL = "ui://u2d86ulcjg9f7";

		public static UI_GuildBossRuleWindow CreateInstance()
		{
			return (UI_GuildBossRuleWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossRuleWindow");
		}

		public UI_GuildBossRuleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_contentList = (GList)this.GetChildAt(4);
			m_closeBtn = (GButton)this.GetChildAt(5);
		}
	}
}