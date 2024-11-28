/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_guildBattleGuildRankItem : GComponent
	{
		public GTextField m_rankNum;
		public GTextField m_guildName;
		public GTextField m_score;
		public GGroup m_scoreGroup;
		public GTextField m_noRankTip;

		public const string URL = "ui://xj95784rpfl42n";

		public static UI_guildBattleGuildRankItem CreateInstance()
		{
			return (UI_guildBattleGuildRankItem)UIPackage.CreateObject("UI_GuildBattle","guildBattleGuildRankItem");
		}

		public UI_guildBattleGuildRankItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rankNum = (GTextField)this.GetChildAt(2);
			m_guildName = (GTextField)this.GetChildAt(3);
			m_score = (GTextField)this.GetChildAt(5);
			m_scoreGroup = (GGroup)this.GetChildAt(6);
			m_noRankTip = (GTextField)this.GetChildAt(7);
		}
	}
}