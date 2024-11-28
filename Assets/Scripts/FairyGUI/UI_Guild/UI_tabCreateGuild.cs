/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_tabCreateGuild : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64f1jg2t";

		public static UI_tabCreateGuild CreateInstance()
		{
			return (UI_tabCreateGuild)UIPackage.CreateObject("UI_Guild","tabCreateGuild");
		}

		public UI_tabCreateGuild()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}