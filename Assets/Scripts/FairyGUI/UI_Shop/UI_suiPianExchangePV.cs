/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_suiPianExchangePV : GComponent
	{
		public GLoader m_imgComsume;
		public GTextField m_txtNum;
		public GTextField m_txtCoinDes;
		public GGroup m_coinGroup;
		public GTextField m_txtTime;
		public GTextField m_txtCount;
		public GComponent m_btnRefresh;
		public GGroup m_refreshGroup;
		public UI_ShopCommonList m_objList;
		public GButton m_btnClose;

		public const string URL = "ui://w9mypx6jlxvz20";

		public static UI_suiPianExchangePV CreateInstance()
		{
			return (UI_suiPianExchangePV)UIPackage.CreateObject("UI_Shop","suiPianExchangePV");
		}

		public UI_suiPianExchangePV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgComsume = (GLoader)this.GetChildAt(8);
			m_txtNum = (GTextField)this.GetChildAt(9);
			m_txtCoinDes = (GTextField)this.GetChildAt(10);
			m_coinGroup = (GGroup)this.GetChildAt(11);
			m_txtTime = (GTextField)this.GetChildAt(14);
			m_txtCount = (GTextField)this.GetChildAt(15);
			m_btnRefresh = (GComponent)this.GetChildAt(16);
			m_refreshGroup = (GGroup)this.GetChildAt(17);
			m_objList = (UI_ShopCommonList)this.GetChildAt(18);
			m_btnClose = (GButton)this.GetChildAt(19);
		}
	}
}