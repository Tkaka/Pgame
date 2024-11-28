/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_itemSellPV : GComponent
	{
		public UI_btnClose m_closeBtn;
		public UI_BagListItem m_icon;
		public GTextField m_itemName;
		public GTextField m_sellNum;
		public GTextField m_sellPrice;
		public GButton m_sellBtn;
		public GButton m_btnMin;
		public GButton m_btnMax;
		public GSlider m_slider;

		public const string URL = "ui://g5pgln3np63ye4d";

		public static UI_itemSellPV CreateInstance()
		{
			return (UI_itemSellPV)UIPackage.CreateObject("UI_Beibao","itemSellPV");
		}

		public UI_itemSellPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (UI_btnClose)this.GetChildAt(3);
			m_icon = (UI_BagListItem)this.GetChildAt(7);
			m_itemName = (GTextField)this.GetChildAt(8);
			m_sellNum = (GTextField)this.GetChildAt(10);
			m_sellPrice = (GTextField)this.GetChildAt(13);
			m_sellBtn = (GButton)this.GetChildAt(14);
			m_btnMin = (GButton)this.GetChildAt(15);
			m_btnMax = (GButton)this.GetChildAt(16);
			m_slider = (GSlider)this.GetChildAt(17);
		}
	}
}