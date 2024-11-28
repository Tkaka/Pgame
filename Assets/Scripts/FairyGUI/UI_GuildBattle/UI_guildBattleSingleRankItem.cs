/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_guildBattleSingleRankItem : GComponent
	{
		public GTextField m_rankNum;
		public GTextField m_rankNum_2;
		public GTextField m_guildName;
		public GTextField m_zhanJi;
		public GTextField m_noRankTip;

		public const string URL = "ui://xj95784rpfl42o";

		public static UI_guildBattleSingleRankItem CreateInstance()
		{
			return (UI_guildBattleSingleRankItem)UIPackage.CreateObject("UI_GuildBattle","guildBattleSingleRankItem");
		}

		public UI_guildBattleSingleRankItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rankNum = (GTextField)this.GetChildAt(1);
			m_rankNum_2 = (GTextField)this.GetChildAt(2);
			m_guildName = (GTextField)this.GetChildAt(3);
			m_zhanJi = (GTextField)this.GetChildAt(4);
			m_noRankTip = (GTextField)this.GetChildAt(5);
		}
	}
}