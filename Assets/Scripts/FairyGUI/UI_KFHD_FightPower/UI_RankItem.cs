/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_RankItem : GComponent
	{
		public GLoader m_rank;
		public GTextField m_rankTxt;
		public GTextField m_name;
		public GTextField m_lvl;
		public GTextField m_fight;

		public const string URL = "ui://9kjh5gh09gdug";

		public static UI_RankItem CreateInstance()
		{
			return (UI_RankItem)UIPackage.CreateObject("UI_KFHD_FightPower","RankItem");
		}

		public UI_RankItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rank = (GLoader)this.GetChildAt(0);
			m_rankTxt = (GTextField)this.GetChildAt(1);
			m_name = (GTextField)this.GetChildAt(2);
			m_lvl = (GTextField)this.GetChildAt(3);
			m_fight = (GTextField)this.GetChildAt(4);
		}
	}
}