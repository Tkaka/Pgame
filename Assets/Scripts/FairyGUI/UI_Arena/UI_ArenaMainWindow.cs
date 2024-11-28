/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ArenaMainWindow : GComponent
	{
		public UI_windowCloseBtn m_btnClose;
		public GComponent m_btnRank;
		public UI_MyRankInfo m_myRankInfo;
		public GComponent m_btnShop;
		public UI_btnScore m_btnScore;
		public GComponent m_btnZhanBao;
		public GComponent m_btnRule;
		public UI_btnReward m_btnReward;
		public GComponent m_btnSet;
		public UI_ArenaList m_List;
		public GButton m_btnLeft;
		public GButton m_btnRight;
		public GTextField m_txtCount;
		public UI_ResetTime m_Reset;
		public GComponent m_btnOneKeyMoBai;
		public UI_ChangePlays m_btnHuanYiPi;
		public UI_AddCount m_ComsumeItem;
		public UI_BuyCount m_buyCount;
		public GComponent m_TaiTou;

		public const string URL = "ui://3xs7lfyxo0dek";

		public static UI_ArenaMainWindow CreateInstance()
		{
			return (UI_ArenaMainWindow)UIPackage.CreateObject("UI_Arena","ArenaMainWindow");
		}

		public UI_ArenaMainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(1);
			m_btnRank = (GComponent)this.GetChildAt(2);
			m_myRankInfo = (UI_MyRankInfo)this.GetChildAt(3);
			m_btnShop = (GComponent)this.GetChildAt(4);
			m_btnScore = (UI_btnScore)this.GetChildAt(5);
			m_btnZhanBao = (GComponent)this.GetChildAt(6);
			m_btnRule = (GComponent)this.GetChildAt(7);
			m_btnReward = (UI_btnReward)this.GetChildAt(8);
			m_btnSet = (GComponent)this.GetChildAt(9);
			m_List = (UI_ArenaList)this.GetChildAt(11);
			m_btnLeft = (GButton)this.GetChildAt(12);
			m_btnRight = (GButton)this.GetChildAt(13);
			m_txtCount = (GTextField)this.GetChildAt(15);
			m_Reset = (UI_ResetTime)this.GetChildAt(16);
			m_btnOneKeyMoBai = (GComponent)this.GetChildAt(17);
			m_btnHuanYiPi = (UI_ChangePlays)this.GetChildAt(18);
			m_ComsumeItem = (UI_AddCount)this.GetChildAt(19);
			m_buyCount = (UI_BuyCount)this.GetChildAt(20);
			m_TaiTou = (GComponent)this.GetChildAt(22);
		}
	}
}