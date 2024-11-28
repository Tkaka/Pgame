/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_tabFindGuild : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64f1jg2u";

		public static UI_tabFindGuild CreateInstance()
		{
			return (UI_tabFindGuild)UIPackage.CreateObject("UI_Guild","tabFindGuild");
		}

		public UI_tabFindGuild()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}