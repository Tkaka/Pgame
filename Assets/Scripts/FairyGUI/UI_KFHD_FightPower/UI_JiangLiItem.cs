/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_JiangLiItem : GComponent
	{
		public GTextField m_rankTxt;
		public GLoader m_rank;
		public GList m_list;

		public const string URL = "ui://9kjh5gh09gdua";

		public static UI_JiangLiItem CreateInstance()
		{
			return (UI_JiangLiItem)UIPackage.CreateObject("UI_KFHD_FightPower","JiangLiItem");
		}

		public UI_JiangLiItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rankTxt = (GTextField)this.GetChildAt(0);
			m_rank = (GLoader)this.GetChildAt(1);
			m_list = (GList)this.GetChildAt(2);
		}
	}
}