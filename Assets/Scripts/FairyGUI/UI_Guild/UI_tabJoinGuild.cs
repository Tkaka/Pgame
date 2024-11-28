/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_tabJoinGuild : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64f1jg2s";

		public static UI_tabJoinGuild CreateInstance()
		{
			return (UI_tabJoinGuild)UIPackage.CreateObject("UI_Guild","tabJoinGuild");
		}

		public UI_tabJoinGuild()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}