/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_activityBtnGroup : GComponent
	{
		public Controller m_activityCtrl;
		public UI_goldBtn m_goldBtn;
		public UI_expBtn m_expBtn;
		public UI_womenBtn m_womenBtn;
		public UI_huanXiangBtn m_huanXiangBtn;
		public Transition m_anim;

		public const string URL = "ui://zwmeip9uff2j1o";

		public static UI_activityBtnGroup CreateInstance()
		{
			return (UI_activityBtnGroup)UIPackage.CreateObject("UI_Activity","activityBtnGroup");
		}

		public UI_activityBtnGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_activityCtrl = this.GetControllerAt(0);
			m_goldBtn = (UI_goldBtn)this.GetChildAt(0);
			m_expBtn = (UI_expBtn)this.GetChildAt(1);
			m_womenBtn = (UI_womenBtn)this.GetChildAt(2);
			m_huanXiangBtn = (UI_huanXiangBtn)this.GetChildAt(3);
			m_anim = this.GetTransitionAt(0);
		}
	}
}