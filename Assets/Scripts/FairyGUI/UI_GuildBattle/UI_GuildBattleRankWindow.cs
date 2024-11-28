/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_GuildBattleRankWindow : GComponent
	{
		public Controller m_ctrl;
		public GTextField m_guildRankLabel;
		public GTextField m_guildScrore;
		public GTextField m_guildScrore_2;
		public GList m_guildRankList;
		public GList m_singleRankList;
		public GComponent m_commonTop;

		public const string URL = "ui://xj95784rpfl42k";

		public static UI_GuildBattleRankWindow CreateInstance()
		{
			return (UI_GuildBattleRankWindow)UIPackage.CreateObject("UI_GuildBattle","GuildBattleRankWindow");
		}

		public UI_GuildBattleRankWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_guildRankLabel = (GTextField)this.GetChildAt(11);
			m_guildScrore = (GTextField)this.GetChildAt(13);
			m_guildScrore_2 = (GTextField)this.GetChildAt(15);
			m_guildRankList = (GList)this.GetChildAt(18);
			m_singleRankList = (GList)this.GetChildAt(21);
			m_commonTop = (GComponent)this.GetChildAt(23);
		}
	}
}