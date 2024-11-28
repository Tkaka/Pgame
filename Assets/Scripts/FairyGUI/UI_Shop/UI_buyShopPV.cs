/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_buyShopPV : GComponent
	{
		public GButton m_btnClose;
		public GTextField m_txtName;
		public GTextField m_txtHaveNum;
		public GButton m_itemIcon;
		public GImage m_imgBg;
		public GTextField m_txtDescribe;
		public GTextField m_txtBuyNum;
		public GLoader m_imgCoin;
		public GTextField m_txtCoinNum;
		public GTextField m_txtRefreshTime;
		public GTextField m_txtTime;
		public GGroup m_supperGroup;
		public GComponent m_btnBuy2;
		public GComponent m_btnRefresh;
		public GComponent m_btnBuy;

		public const string URL = "ui://w9mypx6jlxvz1w";

		public static UI_buyShopPV CreateInstance()
		{
			return (UI_buyShopPV)UIPackage.CreateObject("UI_Shop","buyShopPV");
		}

		public UI_buyShopPV()
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
			m_txtBuyNum = (GTextField)this.GetChildAt(20);
			m_imgCoin = (GLoader)this.GetChildAt(22);
			m_txtCoinNum = (GTextField)this.GetChildAt(23);
			m_txtRefreshTime = (GTextField)this.GetChildAt(27);
			m_txtTime = (GTextField)this.GetChildAt(28);
			m_supperGroup = (GGroup)this.GetChildAt(29);
			m_btnBuy2 = (GComponent)this.GetChildAt(30);
			m_btnRefresh = (GComponent)this.GetChildAt(31);
			m_btnBuy = (GComponent)this.GetChildAt(32);
		}
	}
}