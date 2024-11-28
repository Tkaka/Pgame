/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_RewardWindow : GComponent
	{
		public Controller m_pageControl;
		public UI_windowCloseBtn m_btnClose;
		public UI_PageReward1 m_page1;
		public UI_PageReward2 m_page2;
		public GButton m_btnBestReward;
		public GButton m_btnDailyReward;

		public const string URL = "ui://3xs7lfyxgawd1n";

		public static UI_RewardWindow CreateInstance()
		{
			return (UI_RewardWindow)UIPackage.CreateObject("UI_Arena","RewardWindow");
		}

		public UI_RewardWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_pageControl = this.GetControllerAt(0);
			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(1);
			m_page1 = (UI_PageReward1)this.GetChildAt(2);
			m_page2 = (UI_PageReward2)this.GetChildAt(3);
			m_btnBestReward = (GButton)this.GetChildAt(4);
			m_btnDailyReward = (GButton)this.GetChildAt(5);
		}
	}
}