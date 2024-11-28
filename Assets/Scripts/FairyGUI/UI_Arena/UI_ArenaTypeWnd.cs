/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ArenaTypeWnd : GComponent
	{
		public UI_windowCloseBtn m_btnClose;
		public GList m_mainList;

		public const string URL = "ui://3xs7lfyxfa342k";

		public static UI_ArenaTypeWnd CreateInstance()
		{
			return (UI_ArenaTypeWnd)UIPackage.CreateObject("UI_Arena","ArenaTypeWnd");
		}

		public UI_ArenaTypeWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(1);
			m_mainList = (GList)this.GetChildAt(2);
		}
	}
}