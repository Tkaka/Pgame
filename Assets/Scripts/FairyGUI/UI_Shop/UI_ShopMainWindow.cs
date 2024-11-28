/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_ShopMainWindow : GComponent
	{
		public Controller m_c1;
		public GList m_tabList;
		public UI_shopToggleGroup m_shopToggleGroup;
		public GTextField m_txtTime;
		public GTextField m_txtCount;
		public GComponent m_btnRefresh;
		public GGroup m_refreshGroup;
		public GTextField m_txtNum;
		public GTextField m_txtCoinDes;
		public GLoader m_imgComsume;
		public GGroup m_coinGroup;
		public UI_ShopCommonList m_objList;
		public GGroup m_shopGroup;
		public GComponent m_commonTop;

		public const string URL = "ui://w9mypx6jyzqv0";

		public static UI_ShopMainWindow CreateInstance()
		{
			return (UI_ShopMainWindow)UIPackage.CreateObject("UI_Shop","ShopMainWindow");
		}

		public UI_ShopMainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
			m_tabList = (GList)this.GetChildAt(0);
			m_shopToggleGroup = (UI_shopToggleGroup)this.GetChildAt(3);
			m_txtTime = (GTextField)this.GetChildAt(9);
			m_txtCount = (GTextField)this.GetChildAt(10);
			m_btnRefresh = (GComponent)this.GetChildAt(11);
			m_refreshGroup = (GGroup)this.GetChildAt(12);
			m_txtNum = (GTextField)this.GetChildAt(14);
			m_txtCoinDes = (GTextField)this.GetChildAt(15);
			m_imgComsume = (GLoader)this.GetChildAt(16);
			m_coinGroup = (GGroup)this.GetChildAt(17);
			m_objList = (UI_ShopCommonList)this.GetChildAt(18);
			m_shopGroup = (GGroup)this.GetChildAt(19);
			m_commonTop = (GComponent)this.GetChildAt(21);
		}
	}
}