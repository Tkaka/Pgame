/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_RankWindow : GComponent
	{
		public GButton m_btnClose;
		public GLoader m_imgIcon;
		public GTextField m_txtFirstName;
		public GTextField m_txtFirstLevel;
		public GTextField m_txtFirstSheTuan;
		public GComponent m_btnSeeInfo;
		public GList m_rankList;
		public UI_RankCell m_myInfo;

		public const string URL = "ui://3xs7lfyxgawd1i";

		public static UI_RankWindow CreateInstance()
		{
			return (UI_RankWindow)UIPackage.CreateObject("UI_Arena","RankWindow");
		}

		public UI_RankWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_imgIcon = (GLoader)this.GetChildAt(5);
			m_txtFirstName = (GTextField)this.GetChildAt(6);
			m_txtFirstLevel = (GTextField)this.GetChildAt(7);
			m_txtFirstSheTuan = (GTextField)this.GetChildAt(8);
			m_btnSeeInfo = (GComponent)this.GetChildAt(9);
			m_rankList = (GList)this.GetChildAt(16);
			m_myInfo = (UI_RankCell)this.GetChildAt(17);
		}
	}
}