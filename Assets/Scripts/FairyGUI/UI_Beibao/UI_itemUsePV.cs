/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_itemUsePV : GComponent
	{
		public UI_btnClose m_closeBtn;
		public GButton m_sellBtn;
		public UI_BagListItem m_icon;
		public GTextField m_itemName;
		public GTextField m_sellNum;
		public GButton m_btnMin;
		public GButton m_btnMax;
		public GSlider m_slider;

		public const string URL = "ui://g5pgln3np63ye4e";

		public static UI_itemUsePV CreateInstance()
		{
			return (UI_itemUsePV)UIPackage.CreateObject("UI_Beibao","itemUsePV");
		}

		public UI_itemUsePV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (UI_btnClose)this.GetChildAt(3);
			m_sellBtn = (GButton)this.GetChildAt(7);
			m_icon = (UI_BagListItem)this.GetChildAt(8);
			m_itemName = (GTextField)this.GetChildAt(9);
			m_sellNum = (GTextField)this.GetChildAt(11);
			m_btnMin = (GButton)this.GetChildAt(12);
			m_btnMax = (GButton)this.GetChildAt(13);
			m_slider = (GSlider)this.GetChildAt(14);
		}
	}
}