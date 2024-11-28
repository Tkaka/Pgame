/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleAwardWindow : GComponent
	{
		public GTextField m_lvTxt;
		public GTextField m_exp;
		public GTextField m_gold;
		public GList m_itemList;
		public GTextField m_unGetTipLabel;
		public UI_jingYingStarList m_jingYingStarList;
		public UI_nomalStarList m_normalStarList;

		public const string URL = "ui://028ppdzhjkpz6r";

		public static UI_BattleAwardWindow CreateInstance()
		{
			return (UI_BattleAwardWindow)UIPackage.CreateObject("UI_Battle","BattleAwardWindow");
		}

		public UI_BattleAwardWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lvTxt = (GTextField)this.GetChildAt(3);
			m_exp = (GTextField)this.GetChildAt(7);
			m_gold = (GTextField)this.GetChildAt(9);
			m_itemList = (GList)this.GetChildAt(12);
			m_unGetTipLabel = (GTextField)this.GetChildAt(13);
			m_jingYingStarList = (UI_jingYingStarList)this.GetChildAt(14);
			m_normalStarList = (UI_nomalStarList)this.GetChildAt(15);
		}
	}
}