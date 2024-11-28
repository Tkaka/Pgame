/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_equipToggleGroup : GComponent
	{
		public Controller m_ctrl;
		public UI_blessBtn m_blessBtn;
		public UI_awakenBtn m_awakenBtn;
		public UI_strengthBtn m_strengthBtn;
		public Transition m_anim;

		public const string URL = "ui://8u3gv94nvjwx1s";

		public static UI_equipToggleGroup CreateInstance()
		{
			return (UI_equipToggleGroup)UIPackage.CreateObject("UI_Equip","equipToggleGroup");
		}

		public UI_equipToggleGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_blessBtn = (UI_blessBtn)this.GetChildAt(0);
			m_awakenBtn = (UI_awakenBtn)this.GetChildAt(1);
			m_strengthBtn = (UI_strengthBtn)this.GetChildAt(3);
			m_anim = this.GetTransitionAt(0);
		}
	}
}