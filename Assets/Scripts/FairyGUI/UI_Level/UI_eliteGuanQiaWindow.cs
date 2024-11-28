/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_eliteGuanQiaWindow : GComponent
	{
		public GGraph m_btnClose;
		public GLoader m_imgModel;
		public GTextField m_txtPower;
		public GTextField m_txtCount;
		public GList m_rewardList;
		public UI_StarNum m_imgStar;
		public GButton m_btnSaoDang;
		public GButton m_btnChallange;
		public GTextField m_txtGuaKaName;

		public const string URL = "ui://z04ymz0erpuw18";

		public static UI_eliteGuanQiaWindow CreateInstance()
		{
			return (UI_eliteGuanQiaWindow)UIPackage.CreateObject("UI_Level","eliteGuanQiaWindow");
		}

		public UI_eliteGuanQiaWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GGraph)this.GetChildAt(0);
			m_imgModel = (GLoader)this.GetChildAt(2);
			m_txtPower = (GTextField)this.GetChildAt(6);
			m_txtCount = (GTextField)this.GetChildAt(7);
			m_rewardList = (GList)this.GetChildAt(9);
			m_imgStar = (UI_StarNum)this.GetChildAt(10);
			m_btnSaoDang = (GButton)this.GetChildAt(11);
			m_btnChallange = (GButton)this.GetChildAt(12);
			m_txtGuaKaName = (GTextField)this.GetChildAt(14);
		}
	}
}