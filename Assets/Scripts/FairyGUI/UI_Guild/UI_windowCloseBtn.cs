/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_windowCloseBtn : GButton
	{
		public Transition m_t0;

		public const string URL = "ui://oe7ras64qbwu0";

		public static UI_windowCloseBtn CreateInstance()
		{
			return (UI_windowCloseBtn)UIPackage.CreateObject("UI_Guild","windowCloseBtn");
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