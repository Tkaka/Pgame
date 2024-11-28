/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_SellCoinItemWnd : GComponent
	{
		public GGraph m_mask;
		public GButton m_btnClose;
		public GList m_itemList;
		public GLoader m_imgCoin;
		public GTextField m_txtCoinNum;
		public GComponent m_btnOk;

		public const string URL = "ui://w9mypx6jijxi21";

		public static UI_SellCoinItemWnd CreateInstance()
		{
			return (UI_SellCoinItemWnd)UIPackage.CreateObject("UI_Shop","SellCoinItemWnd");
		}

		public UI_SellCoinItemWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_btnClose = (GButton)this.GetChildAt(7);
			m_itemList = (GList)this.GetChildAt(9);
			m_imgCoin = (GLoader)this.GetChildAt(13);
			m_txtCoinNum = (GTextField)this.GetChildAt(14);
			m_btnOk = (GComponent)this.GetChildAt(16);
		}
	}
}