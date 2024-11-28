/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_GuildBattleExchangeWindow : GComponent
	{
		public GList m_exchangeList;
		public GButton m_item2;
		public GTextField m_havaNumLabel2;
		public GButton m_item1;
		public GTextField m_havaNumLabel1;

		public const string URL = "ui://xj95784rpfl42s";

		public static UI_GuildBattleExchangeWindow CreateInstance()
		{
			return (UI_GuildBattleExchangeWindow)UIPackage.CreateObject("UI_GuildBattle","GuildBattleExchangeWindow");
		}

		public UI_GuildBattleExchangeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_exchangeList = (GList)this.GetChildAt(2);
			m_item2 = (GButton)this.GetChildAt(5);
			m_havaNumLabel2 = (GTextField)this.GetChildAt(6);
			m_item1 = (GButton)this.GetChildAt(7);
			m_havaNumLabel1 = (GTextField)this.GetChildAt(8);
		}
	}
}