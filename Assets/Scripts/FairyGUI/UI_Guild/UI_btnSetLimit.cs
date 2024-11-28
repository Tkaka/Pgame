/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnSetLimit : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92i";

		public static UI_btnSetLimit CreateInstance()
		{
			return (UI_btnSetLimit)UIPackage.CreateObject("UI_Guild","btnSetLimit");
		}

		public UI_btnSetLimit()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}