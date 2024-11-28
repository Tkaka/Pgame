/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_guildBattleExchangeItem : GComponent
	{
		public GButton m_costItem;
		public GButton m_exchangeItem;
		public GButton m_exchangeBtn;

		public const string URL = "ui://xj95784rpfl42t";

		public static UI_guildBattleExchangeItem CreateInstance()
		{
			return (UI_guildBattleExchangeItem)UIPackage.CreateObject("UI_GuildBattle","guildBattleExchangeItem");
		}

		public UI_guildBattleExchangeItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_costItem = (GButton)this.GetChildAt(1);
			m_exchangeItem = (GButton)this.GetChildAt(2);
			m_exchangeBtn = (GButton)this.GetChildAt(3);
		}
	}
}