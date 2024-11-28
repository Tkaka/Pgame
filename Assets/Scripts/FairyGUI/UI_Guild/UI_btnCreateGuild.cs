/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnCreateGuild : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64f1jg35";

		public static UI_btnCreateGuild CreateInstance()
		{
			return (UI_btnCreateGuild)UIPackage.CreateObject("UI_Guild","btnCreateGuild");
		}

		public UI_btnCreateGuild()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}