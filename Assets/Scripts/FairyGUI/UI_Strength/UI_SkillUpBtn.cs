/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_SkillUpBtn : GButton
	{
		public GImage m_addBtn;

		public const string URL = "ui://qnd9tp35vhpp3p";

		public static UI_SkillUpBtn CreateInstance()
		{
			return (UI_SkillUpBtn)UIPackage.CreateObject("UI_Strength","SkillUpBtn");
		}

		public UI_SkillUpBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_addBtn = (GImage)this.GetChildAt(0);
		}
	}
}