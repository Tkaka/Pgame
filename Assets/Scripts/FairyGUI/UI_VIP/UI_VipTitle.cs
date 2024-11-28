/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_VIP
{
	public partial class UI_VipTitle : GComponent
	{
		public GTextField m_vipLevel;
		public GTextField m_rechargeNum;
		public GTextField m_txtNextLevel;
		public GGroup m_objGroup;
		public GButton m_btnRecharge;
		public GProgressBar m_expBar;
		public GTextField m_txtExpNum;
		public GButton m_btnVip;

		public const string URL = "ui://7pvd5vi49wdbi";

		public static UI_VipTitle CreateInstance()
		{
			return (UI_VipTitle)UIPackage.CreateObject("UI_VIP","VipTitle");
		}

		public UI_VipTitle()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_vipLevel = (GTextField)this.GetChildAt(3);
			m_rechargeNum = (GTextField)this.GetChildAt(6);
			m_txtNextLevel = (GTextField)this.GetChildAt(9);
			m_objGroup = (GGroup)this.GetChildAt(11);
			m_btnRecharge = (GButton)this.GetChildAt(12);
			m_expBar = (GProgressBar)this.GetChildAt(13);
			m_txtExpNum = (GTextField)this.GetChildAt(14);
			m_btnVip = (GButton)this.GetChildAt(15);
		}
	}
}