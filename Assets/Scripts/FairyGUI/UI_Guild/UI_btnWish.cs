/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnWish : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64vmh3b3r";

		public static UI_btnWish CreateInstance()
		{
			return (UI_btnWish)UIPackage.CreateObject("UI_Guild","btnWish");
		}

		public UI_btnWish()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}