/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Mail
{
	public partial class UI_MailContentWnd : GComponent
	{
		public GButton m_btnClose;
		public GTextField m_txtTitle;
		public GTextField m_txtDescribe;
		public GTextField m_txtFrom;
		public GComponent m_btnGet;
		public GList m_rewardList;
		public GComponent m_imgGeted;
		public GGroup m_rewardGroup;

		public const string URL = "ui://wgl1vubmyzqv9";

		public static UI_MailContentWnd CreateInstance()
		{
			return (UI_MailContentWnd)UIPackage.CreateObject("UI_Mail","MailContentWnd");
		}

		public UI_MailContentWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(2);
			m_txtTitle = (GTextField)this.GetChildAt(3);
			m_txtDescribe = (GTextField)this.GetChildAt(4);
			m_txtFrom = (GTextField)this.GetChildAt(5);
			m_btnGet = (GComponent)this.GetChildAt(7);
			m_rewardList = (GList)this.GetChildAt(8);
			m_imgGeted = (GComponent)this.GetChildAt(9);
			m_rewardGroup = (GGroup)this.GetChildAt(10);
		}
	}
}