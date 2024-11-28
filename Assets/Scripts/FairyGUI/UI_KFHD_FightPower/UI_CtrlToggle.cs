/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_CtrlToggle : GLabel
	{
		public Controller m_c1;

		public const string URL = "ui://9kjh5gh09gdu6";

		public static UI_CtrlToggle CreateInstance()
		{
			return (UI_CtrlToggle)UIPackage.CreateObject("UI_KFHD_FightPower","CtrlToggle");
		}

		public UI_CtrlToggle()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
		}
	}
}