/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_DescItemSkill : GComponent
	{
		public GLoader m_bg;
		public GLoader m_icon;

		public const string URL = "ui://9kjh5gh09gduj";

		public static UI_DescItemSkill CreateInstance()
		{
			return (UI_DescItemSkill)UIPackage.CreateObject("UI_KFHD_FightPower","DescItemSkill");
		}

		public UI_DescItemSkill()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GLoader)this.GetChildAt(0);
			m_icon = (GLoader)this.GetChildAt(1);
		}
	}
}