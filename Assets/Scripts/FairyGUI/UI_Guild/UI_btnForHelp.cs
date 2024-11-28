/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnForHelp : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64vmh3b3q";

		public static UI_btnForHelp CreateInstance()
		{
			return (UI_btnForHelp)UIPackage.CreateObject("UI_Guild","btnForHelp");
		}

		public UI_btnForHelp()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}