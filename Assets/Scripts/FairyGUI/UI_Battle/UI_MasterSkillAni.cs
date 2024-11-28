/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_MasterSkillAni : GComponent
	{
		public GLoader m_headImg;
		public GLoader m_skillName;
		public Transition m_t0;

		public const string URL = "ui://028ppdzhemyvde";

		public static UI_MasterSkillAni CreateInstance()
		{
			return (UI_MasterSkillAni)UIPackage.CreateObject("UI_Battle","MasterSkillAni");
		}

		public UI_MasterSkillAni()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_headImg = (GLoader)this.GetChildAt(3);
			m_skillName = (GLoader)this.GetChildAt(5);
			m_t0 = this.GetTransitionAt(0);
		}
	}
}