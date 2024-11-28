/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_GuildLogWnd : GComponent
	{
		public GList m_LogList;

		public const string URL = "ui://oe7ras64f1jg2p";

		public static UI_GuildLogWnd CreateInstance()
		{
			return (UI_GuildLogWnd)UIPackage.CreateObject("UI_Guild","GuildLogWnd");
		}

		public UI_GuildLogWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_LogList = (GList)this.GetChildAt(0);
		}
	}
}