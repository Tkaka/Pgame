/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_SkillPanel : GComponent
	{
		public GImage m_skillIcon;
		public GTextField m_skillName;
		public GTextField m_hurtTxt;
		public GTextField m_hurtNumber;

		public const string URL = "ui://028ppdzhlz8s5j";

		public static UI_SkillPanel CreateInstance()
		{
			return (UI_SkillPanel)UIPackage.CreateObject("UI_Battle","SkillPanel");
		}

		public UI_SkillPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_skillIcon = (GImage)this.GetChildAt(1);
			m_skillName = (GTextField)this.GetChildAt(2);
			m_hurtTxt = (GTextField)this.GetChildAt(3);
			m_hurtNumber = (GTextField)this.GetChildAt(4);
		}
	}
}