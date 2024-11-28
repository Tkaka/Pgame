/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Talent
{
	public partial class UI_windowCloseBtn : GButton
	{
		public Transition m_t0;

		public const string URL = "ui://erk5lfvwm6aab";

		public static UI_windowCloseBtn CreateInstance()
		{
			return (UI_windowCloseBtn)UIPackage.CreateObject("UI_Talent","windowCloseBtn");
		}

		public UI_windowCloseBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_t0 = this.GetTransitionAt(0);
		}
	}
}