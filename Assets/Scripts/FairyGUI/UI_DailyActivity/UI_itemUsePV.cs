/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_itemUsePV : GComponent
	{
		public UI_btnClose m_closeBtn;
		public GButton m_sellBtn;
		public GTextField m_itemName;
		public GTextField m_sellNum;
		public GButton m_btnMin;
		public GButton m_btnMax;
		public GSlider m_slider;
		public GTextField m_place;

		public const string URL = "ui://0n5r1ymrl73xf";

		public static UI_itemUsePV CreateInstance()
		{
			return (UI_itemUsePV)UIPackage.CreateObject("UI_DailyActivity","itemUsePV");
		}

		public UI_itemUsePV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (UI_btnClose)this.GetChildAt(3);
			m_sellBtn = (GButton)this.GetChildAt(7);
			m_itemName = (GTextField)this.GetChildAt(8);
			m_sellNum = (GTextField)this.GetChildAt(10);
			m_btnMin = (GButton)this.GetChildAt(11);
			m_btnMax = (GButton)this.GetChildAt(12);
			m_slider = (GSlider)this.GetChildAt(13);
			m_place = (GTextField)this.GetChildAt(14);
		}
	}
}