/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnQuit : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64qbwum";

		public static UI_btnQuit CreateInstance()
		{
			return (UI_btnQuit)UIPackage.CreateObject("UI_Guild","btnQuit");
		}

		public UI_btnQuit()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}