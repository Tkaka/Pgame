/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_toggleGroup : GComponent
	{
		public Controller m_ctrl;
		public UI_zhanHunBtn m_zhanHunBtn;
		public UI_jiNengBtn m_skillBtn;
		public UI_starUpBtn m_starUpBtn;
		public UI_shengJiBtn m_levelUpBtn;
		public UI_shengPingBtn m_shengPingBtn;
		public Transition m_anim;

		public const string URL = "ui://qnd9tp35vjwx4k";

		public static UI_toggleGroup CreateInstance()
		{
			return (UI_toggleGroup)UIPackage.CreateObject("UI_Strength","toggleGroup");
		}

		public UI_toggleGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_zhanHunBtn = (UI_zhanHunBtn)this.GetChildAt(0);
			m_skillBtn = (UI_jiNengBtn)this.GetChildAt(1);
			m_starUpBtn = (UI_starUpBtn)this.GetChildAt(2);
			m_levelUpBtn = (UI_shengJiBtn)this.GetChildAt(3);
			m_shengPingBtn = (UI_shengPingBtn)this.GetChildAt(5);
			m_anim = this.GetTransitionAt(0);
		}
	}
}