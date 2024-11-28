/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_ChangeBadgeWnd : GComponent
	{
		public GButton m_btnClose;
		public GList m_badgeList;

		public const string URL = "ui://oe7ras64fde923";

		public static UI_ChangeBadgeWnd CreateInstance()
		{
			return (UI_ChangeBadgeWnd)UIPackage.CreateObject("UI_Guild","ChangeBadgeWnd");
		}

		public UI_ChangeBadgeWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_badgeList = (GList)this.GetChildAt(4);
		}
	}
}