/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossRewradWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_contentList;
		public GButton m_closeBtn;

		public const string URL = "ui://u2d86ulcjg9fe";

		public static UI_GuildBossRewradWindow CreateInstance()
		{
			return (UI_GuildBossRewradWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossRewradWindow");
		}

		public UI_GuildBossRewradWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_contentList = (GList)this.GetChildAt(5);
			m_closeBtn = (GButton)this.GetChildAt(6);
		}
	}
}