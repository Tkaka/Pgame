/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_TabJiangLi : GComponent
	{
		public GTextField m_desc;
		public GList m_list;

		public const string URL = "ui://9kjh5gh09gdu9";

		public static UI_TabJiangLi CreateInstance()
		{
			return (UI_TabJiangLi)UIPackage.CreateObject("UI_KFHD_FightPower","TabJiangLi");
		}

		public UI_TabJiangLi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_desc = (GTextField)this.GetChildAt(0);
			m_list = (GList)this.GetChildAt(1);
		}
	}
}