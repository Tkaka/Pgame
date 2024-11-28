/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_TabRank : GComponent
	{
		public GTextField m_conditionTxt;
		public GGroup m_lowLevel;
		public GList m_list;

		public const string URL = "ui://9kjh5gh09gdud";

		public static UI_TabRank CreateInstance()
		{
			return (UI_TabRank)UIPackage.CreateObject("UI_KFHD_FightPower","TabRank");
		}

		public UI_TabRank()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_conditionTxt = (GTextField)this.GetChildAt(0);
			m_lowLevel = (GGroup)this.GetChildAt(2);
			m_list = (GList)this.GetChildAt(3);
		}
	}
}