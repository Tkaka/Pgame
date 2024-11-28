/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossPassRankWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_passRankList;
		public GTextField m_selfRankLabel;
		public GTextField m_selfGuildNameLabel;
		public GTextField m_selfPassTimeLabel;
		public GTextField m_unpassTipLabel;
		public GButton m_closeBtn;

		public const string URL = "ui://u2d86ulcjg9f1";

		public static UI_GuildBossPassRankWindow CreateInstance()
		{
			return (UI_GuildBossPassRankWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossPassRankWindow");
		}

		public UI_GuildBossPassRankWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_passRankList = (GList)this.GetChildAt(9);
			m_selfRankLabel = (GTextField)this.GetChildAt(10);
			m_selfGuildNameLabel = (GTextField)this.GetChildAt(11);
			m_selfPassTimeLabel = (GTextField)this.GetChildAt(12);
			m_unpassTipLabel = (GTextField)this.GetChildAt(13);
			m_closeBtn = (GButton)this.GetChildAt(14);
		}
	}
}