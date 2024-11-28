/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_suiPianBuyPV : GComponent
	{
		public GButton m_btnClose;
		public GTextField m_txtName;
		public GTextField m_txtHaveNum;
		public GButton m_itemIcon;
		public GImage m_imgBg;
		public GTextField m_txtDescribe;
		public GLoader m_imgCoin;
		public GButton m_btnSub;
		public GButton m_btnAdd;
		public GComponent m_btnAddMax;
		public GTextField m_txtCoinNum;
		public GTextField m_txtNum;
		public GComponent m_btnBuy;

		public const string URL = "ui://w9mypx6jlxvz1z";

		public static UI_suiPianBuyPV CreateInstance()
		{
			return (UI_suiPianBuyPV)UIPackage.CreateObject("UI_Shop","suiPianBuyPV");
		}

		public UI_suiPianBuyPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(6);
			m_txtName = (GTextField)this.GetChildAt(7);
			m_txtHaveNum = (GTextField)this.GetChildAt(9);
			m_itemIcon = (GButton)this.GetChildAt(12);
			m_imgBg = (GImage)this.GetChildAt(14);
			m_txtDescribe = (GTextField)this.GetChildAt(15);
			m_imgCoin = (GLoader)this.GetChildAt(19);
			m_btnSub = (GButton)this.GetChildAt(20);
			m_btnAdd = (GButton)this.GetChildAt(21);
			m_btnAddMax = (GComponent)this.GetChildAt(23);
			m_txtCoinNum = (GTextField)this.GetChildAt(25);
			m_txtNum = (GTextField)this.GetChildAt(27);
			m_btnBuy = (GComponent)this.GetChildAt(29);
		}
	}
}