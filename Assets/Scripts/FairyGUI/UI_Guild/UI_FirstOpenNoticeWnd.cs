/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_FirstOpenNoticeWnd : GComponent
	{
		public GButton m_btnClose;
		public GTextField m_txtChairMan;
		public GTextField m_txtNotice;

		public const string URL = "ui://oe7ras64facbb3j";

		public static UI_FirstOpenNoticeWnd CreateInstance()
		{
			return (UI_FirstOpenNoticeWnd)UIPackage.CreateObject("UI_Guild","FirstOpenNoticeWnd");
		}

		public UI_FirstOpenNoticeWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_txtChairMan = (GTextField)this.GetChildAt(4);
			m_txtNotice = (GTextField)this.GetChildAt(5);
		}
	}
}