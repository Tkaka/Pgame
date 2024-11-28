/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossDistributeWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_contentList;
		public GButton m_closeBtn;

		public const string URL = "ui://u2d86ulcjg9fm";

		public static UI_GuildBossDistributeWindow CreateInstance()
		{
			return (UI_GuildBossDistributeWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossDistributeWindow");
		}

		public UI_GuildBossDistributeWindow()
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