/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_taskToggleGroup : GComponent
	{
		public Controller m_ctrl;
		public UI_RiChangRenWuBtn m_RiChangRenWuBtn;
		public UI_ZhuXianRenWuBtn m_ZhuXianRenWuBtn;
		public Transition m_anim;

		public const string URL = "ui://zswzat1emsjvw";

		public static UI_taskToggleGroup CreateInstance()
		{
			return (UI_taskToggleGroup)UIPackage.CreateObject("UI_TaskSystem","taskToggleGroup");
		}

		public UI_taskToggleGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_RiChangRenWuBtn = (UI_RiChangRenWuBtn)this.GetChildAt(0);
			m_ZhuXianRenWuBtn = (UI_ZhuXianRenWuBtn)this.GetChildAt(2);
			m_anim = this.GetTransitionAt(0);
		}
	}
}