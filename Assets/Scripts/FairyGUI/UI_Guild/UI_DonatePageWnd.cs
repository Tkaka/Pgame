/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_DonatePageWnd : GComponent
	{
		public UI_windowCloseBtn m_btnClose;
		public GList m_donateList;
		public GLoader m_imgTitle;
		public GTextField m_txtShuoMing;

		public const string URL = "ui://oe7ras64nos7b46";

		public static UI_DonatePageWnd CreateInstance()
		{
			return (UI_DonatePageWnd)UIPackage.CreateObject("UI_Guild","DonatePageWnd");
		}

		public UI_DonatePageWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(2);
			m_donateList = (GList)this.GetChildAt(3);
			m_imgTitle = (GLoader)this.GetChildAt(4);
			m_txtShuoMing = (GTextField)this.GetChildAt(5);
		}
	}
}