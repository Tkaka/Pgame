/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Mail
{
	public partial class UI_MailWnd : GComponent
	{
		public GButton m_btnClose;
		public GTextField m_txtMailNum;
		public GList m_mainList;
		public GComponent m_btnOneKeyGet;

		public const string URL = "ui://wgl1vubmyzqv0";

		public static UI_MailWnd CreateInstance()
		{
			return (UI_MailWnd)UIPackage.CreateObject("UI_Mail","MailWnd");
		}

		public UI_MailWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_txtMailNum = (GTextField)this.GetChildAt(2);
			m_mainList = (GList)this.GetChildAt(6);
			m_btnOneKeyGet = (GComponent)this.GetChildAt(7);
		}
	}
}