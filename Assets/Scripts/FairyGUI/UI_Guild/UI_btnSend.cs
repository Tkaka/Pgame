/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnSend : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde922";

		public static UI_btnSend CreateInstance()
		{
			return (UI_btnSend)UIPackage.CreateObject("UI_Guild","btnSend");
		}

		public UI_btnSend()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}