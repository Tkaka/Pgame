/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_DonateHallWnd : GComponent
	{
		public UI_windowCloseBtn m_btnClose;
		public GTextField m_txtGuildLevel;
		public UI_objProgressBar2 m_totalPorgressBar;
		public UI_objProgressBar2 m_tadayProgressBar;
		public GTextField m_txtDonateCount;
		public UI_btnDonate m_btnDonate;
		public GList m_donateList;

		public const string URL = "ui://oe7ras64lcbob3z";

		public static UI_DonateHallWnd CreateInstance()
		{
			return (UI_DonateHallWnd)UIPackage.CreateObject("UI_Guild","DonateHallWnd");
		}

		public UI_DonateHallWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(2);
			m_txtGuildLevel = (GTextField)this.GetChildAt(4);
			m_totalPorgressBar = (UI_objProgressBar2)this.GetChildAt(5);
			m_tadayProgressBar = (UI_objProgressBar2)this.GetChildAt(6);
			m_txtDonateCount = (GTextField)this.GetChildAt(9);
			m_btnDonate = (UI_btnDonate)this.GetChildAt(10);
			m_donateList = (GList)this.GetChildAt(11);
		}
	}
}