/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_JinHuaSuccessWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_okBtn;
		public GComponent m_petHead;
		public GTextField m_oldHp;
		public GTextField m_newHp;
		public GTextField m_oldDef;
		public GTextField m_newDef;
		public GTextField m_oldAtk;
		public GTextField m_newAtk;
		public GTextField m_oldFightPower;
		public GTextField m_newFightPower;
		public GLoader m_boardIcon;
		public GLoader m_skillIcon;
		public GTextField m_unlockSkillTxt;
		public GTextField m_skillDesc;
		public GTextField m_oldXianShouZhi;
		public GTextField m_newXianShouZhi;
		public GComponent m_rightStarAnim;
		public GComponent m_leftStarAnim;
		public Transition m_anim;

		public const string URL = "ui://qnd9tp35n1c022";

		public static UI_JinHuaSuccessWindow CreateInstance()
		{
			return (UI_JinHuaSuccessWindow)UIPackage.CreateObject("UI_Strength","JinHuaSuccessWindow");
		}

		public UI_JinHuaSuccessWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_okBtn = (GButton)this.GetChildAt(4);
			m_petHead = (GComponent)this.GetChildAt(6);
			m_oldHp = (GTextField)this.GetChildAt(8);
			m_newHp = (GTextField)this.GetChildAt(10);
			m_oldDef = (GTextField)this.GetChildAt(13);
			m_newDef = (GTextField)this.GetChildAt(15);
			m_oldAtk = (GTextField)this.GetChildAt(18);
			m_newAtk = (GTextField)this.GetChildAt(20);
			m_oldFightPower = (GTextField)this.GetChildAt(23);
			m_newFightPower = (GTextField)this.GetChildAt(25);
			m_boardIcon = (GLoader)this.GetChildAt(27);
			m_skillIcon = (GLoader)this.GetChildAt(28);
			m_unlockSkillTxt = (GTextField)this.GetChildAt(29);
			m_skillDesc = (GTextField)this.GetChildAt(30);
			m_oldXianShouZhi = (GTextField)this.GetChildAt(32);
			m_newXianShouZhi = (GTextField)this.GetChildAt(34);
			m_rightStarAnim = (GComponent)this.GetChildAt(36);
			m_leftStarAnim = (GComponent)this.GetChildAt(37);
			m_anim = this.GetTransitionAt(0);
		}
	}
}