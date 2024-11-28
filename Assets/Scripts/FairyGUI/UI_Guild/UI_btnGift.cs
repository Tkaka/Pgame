/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnGift : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64vmh3b3s";

		public static UI_btnGift CreateInstance()
		{
			return (UI_btnGift)UIPackage.CreateObject("UI_Guild","btnGift");
		}

		public UI_btnGift()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}