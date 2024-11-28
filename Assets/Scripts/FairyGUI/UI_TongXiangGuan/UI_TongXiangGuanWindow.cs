/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_TongXiangGuanWindow : GComponent
	{
		public GList m_tongXiangPageList;
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;
		public GTextField m_totalValueLabel;
		public GList m_pageTipList;
		public GButton m_paiHangBtn;
		public GButton m_ruleBtn;
		public GButton m_backBtn;

		public const string URL = "ui://ansp6fm5lni70";

		public static UI_TongXiangGuanWindow CreateInstance()
		{
			return (UI_TongXiangGuanWindow)UIPackage.CreateObject("UI_TongXiangGuan","TongXiangGuanWindow");
		}

		public UI_TongXiangGuanWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tongXiangPageList = (GList)this.GetChildAt(0);
			m_switchLeftBtn = (GButton)this.GetChildAt(1);
			m_switchRightBtn = (GButton)this.GetChildAt(2);
			m_totalValueLabel = (GTextField)this.GetChildAt(6);
			m_pageTipList = (GList)this.GetChildAt(7);
			m_paiHangBtn = (GButton)this.GetChildAt(8);
			m_ruleBtn = (GButton)this.GetChildAt(9);
			m_backBtn = (GButton)this.GetChildAt(12);
		}
	}
}