/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_DonateRewardWnd : GComponent
	{
		public GTextField m_txtGuildLevel;
		public UI_objProgressBar2 m_totalPorgressBar;
		public GList m_donateList;
		public GButton m_btnClose;
		public UI_badgeCell m_imgBadge;
		public GTextField m_txtDonateCount;
		public GTextField m_txtType;

		public const string URL = "ui://oe7ras64lcbob43";

		public static UI_DonateRewardWnd CreateInstance()
		{
			return (UI_DonateRewardWnd)UIPackage.CreateObject("UI_Guild","DonateRewardWnd");
		}

		public UI_DonateRewardWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtGuildLevel = (GTextField)this.GetChildAt(2);
			m_totalPorgressBar = (UI_objProgressBar2)this.GetChildAt(3);
			m_donateList = (GList)this.GetChildAt(5);
			m_btnClose = (GButton)this.GetChildAt(6);
			m_imgBadge = (UI_badgeCell)this.GetChildAt(7);
			m_txtDonateCount = (GTextField)this.GetChildAt(8);
			m_txtType = (GTextField)this.GetChildAt(10);
		}
	}
}