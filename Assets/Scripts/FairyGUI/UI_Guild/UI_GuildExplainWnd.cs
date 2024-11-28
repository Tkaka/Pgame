/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_GuildExplainWnd : GComponent
	{
		public GButton m_btnClose;
		public GList m_txtList;

		public const string URL = "ui://oe7ras64fde927";

		public static UI_GuildExplainWnd CreateInstance()
		{
			return (UI_GuildExplainWnd)UIPackage.CreateObject("UI_Guild","GuildExplainWnd");
		}

		public UI_GuildExplainWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_txtList = (GList)this.GetChildAt(4);
		}
	}
}