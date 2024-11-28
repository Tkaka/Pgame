/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_FightJiangLiItem : GComponent
	{
		public GTextField m_fightTxt;
		public GList m_list;

		public const string URL = "ui://9kjh5gh09gduc";

		public static UI_FightJiangLiItem CreateInstance()
		{
			return (UI_FightJiangLiItem)UIPackage.CreateObject("UI_KFHD_FightPower","FightJiangLiItem");
		}

		public UI_FightJiangLiItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_fightTxt = (GTextField)this.GetChildAt(0);
			m_list = (GList)this.GetChildAt(1);
		}
	}
}