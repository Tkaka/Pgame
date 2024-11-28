/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnDonate : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64lcbob41";

		public static UI_btnDonate CreateInstance()
		{
			return (UI_btnDonate)UIPackage.CreateObject("UI_Guild","btnDonate");
		}

		public UI_btnDonate()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}