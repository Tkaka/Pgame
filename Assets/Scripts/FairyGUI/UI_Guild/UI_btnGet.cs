/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnGet : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64f1jg3a";

		public static UI_btnGet CreateInstance()
		{
			return (UI_btnGet)UIPackage.CreateObject("UI_Guild","btnGet");
		}

		public UI_btnGet()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}