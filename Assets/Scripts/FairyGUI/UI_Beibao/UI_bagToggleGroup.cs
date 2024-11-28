/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_bagToggleGroup : GComponent
	{
		public Controller m_ctrl;
		public GButton m_btnEquip;
		public GButton m_btnCaiLiao;
		public GButton m_btnSuiPian;
		public GButton m_btnComsume;
		public UI_SBg m_imgBg;
		public GButton m_btnAll;
		public Transition m_anim;

		public const string URL = "ui://g5pgln3nboo2e4b";

		public static UI_bagToggleGroup CreateInstance()
		{
			return (UI_bagToggleGroup)UIPackage.CreateObject("UI_Beibao","bagToggleGroup");
		}

		public UI_bagToggleGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_btnEquip = (GButton)this.GetChildAt(0);
			m_btnCaiLiao = (GButton)this.GetChildAt(1);
			m_btnSuiPian = (GButton)this.GetChildAt(2);
			m_btnComsume = (GButton)this.GetChildAt(3);
			m_imgBg = (UI_SBg)this.GetChildAt(4);
			m_btnAll = (GButton)this.GetChildAt(5);
			m_anim = this.GetTransitionAt(0);
		}
	}
}