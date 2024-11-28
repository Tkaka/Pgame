/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_TabFightJiangLi : GComponent
	{
		public GTextField m_desc;
		public GList m_list;

		public const string URL = "ui://9kjh5gh09gdub";

		public static UI_TabFightJiangLi CreateInstance()
		{
			return (UI_TabFightJiangLi)UIPackage.CreateObject("UI_KFHD_FightPower","TabFightJiangLi");
		}

		public UI_TabFightJiangLi()
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