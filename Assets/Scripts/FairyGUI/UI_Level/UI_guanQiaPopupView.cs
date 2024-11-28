/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_guanQiaPopupView : GComponent
	{
		public GGraph m_btnMask;
		public GTextField m_txtLevelTarget;
		public GTextField m_txtBossDescribe;
		public GTextField m_txtComsume;
		public GLoader m_imgGuanQia;
		public GTextField m_txtComsumeNum;
		public GList m_rewardList;
		public GButton m_btnSaoDang50;
		public GButton m_btnSaoDang10;
		public GButton m_btnSaoDang;
		public GButton m_btnChallange;
		public GTextField m_txtGuanQia;
		public UI_StarNum m_imgStar;
		public GLoader m_lihuiLoader;

		public const string URL = "ui://z04ymz0ew50m25k";

		public static UI_guanQiaPopupView CreateInstance()
		{
			return (UI_guanQiaPopupView)UIPackage.CreateObject("UI_Level","guanQiaPopupView");
		}

		public UI_guanQiaPopupView()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnMask = (GGraph)this.GetChildAt(1);
			m_txtLevelTarget = (GTextField)this.GetChildAt(5);
			m_txtBossDescribe = (GTextField)this.GetChildAt(6);
			m_txtComsume = (GTextField)this.GetChildAt(7);
			m_imgGuanQia = (GLoader)this.GetChildAt(9);
			m_txtComsumeNum = (GTextField)this.GetChildAt(10);
			m_rewardList = (GList)this.GetChildAt(12);
			m_btnSaoDang50 = (GButton)this.GetChildAt(15);
			m_btnSaoDang10 = (GButton)this.GetChildAt(16);
			m_btnSaoDang = (GButton)this.GetChildAt(17);
			m_btnChallange = (GButton)this.GetChildAt(18);
			m_txtGuanQia = (GTextField)this.GetChildAt(21);
			m_imgStar = (UI_StarNum)this.GetChildAt(22);
			m_lihuiLoader = (GLoader)this.GetChildAt(23);
		}
	}
}