/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_WndKFHDFightPower : GComponent
	{
		public GTextField m_txtLeftTime;
		public GButton m_clsBtn;
		public UI_CtrlToggle m_ctrl;

		public const string URL = "ui://9kjh5gh09gdu0";

		public static UI_WndKFHDFightPower CreateInstance()
		{
			return (UI_WndKFHDFightPower)UIPackage.CreateObject("UI_KFHD_FightPower","WndKFHDFightPower");
		}

		public UI_WndKFHDFightPower()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtLeftTime = (GTextField)this.GetChildAt(2);
			m_clsBtn = (GButton)this.GetChildAt(3);
			m_ctrl = (UI_CtrlToggle)this.GetChildAt(4);
		}
	}
}