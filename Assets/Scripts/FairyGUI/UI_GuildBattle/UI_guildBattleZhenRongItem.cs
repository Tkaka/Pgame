/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_guildBattleZhenRongItem : GComponent
	{
		public GTextField m_teamNum;
		public GTextField m_fightPower;
		public GTextField m_xianShouZhi;
		public GList m_petList;

		public const string URL = "ui://xj95784rmtsg2g";

		public static UI_guildBattleZhenRongItem CreateInstance()
		{
			return (UI_guildBattleZhenRongItem)UIPackage.CreateObject("UI_GuildBattle","guildBattleZhenRongItem");
		}

		public UI_guildBattleZhenRongItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_teamNum = (GTextField)this.GetChildAt(2);
			m_fightPower = (GTextField)this.GetChildAt(4);
			m_xianShouZhi = (GTextField)this.GetChildAt(6);
			m_petList = (GList)this.GetChildAt(8);
		}
	}
}