/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnAgree : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92h";

		public static UI_btnAgree CreateInstance()
		{
			return (UI_btnAgree)UIPackage.CreateObject("UI_Guild","btnAgree");
		}

		public UI_btnAgree()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}