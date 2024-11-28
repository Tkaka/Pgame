/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_itemComposePV : GComponent
	{
		public UI_btnClose m_closeBtn;
		public GButton m_icon;
		public GTextField m_itemName;
		public GTextField m_sellNum;
		public GButton m_btnMin;
		public GButton m_btnMax;
		public GSlider m_slider;
		public GButton m_btnCompose;
		public GTextField m_txtDes;

		public const string URL = "ui://g5pgln3np63ye4c";

		public static UI_itemComposePV CreateInstance()
		{
			return (UI_itemComposePV)UIPackage.CreateObject("UI_Beibao","itemComposePV");
		}

		public UI_itemComposePV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (UI_btnClose)this.GetChildAt(3);
			m_icon = (GButton)this.GetChildAt(7);
			m_itemName = (GTextField)this.GetChildAt(8);
			m_sellNum = (GTextField)this.GetChildAt(10);
			m_btnMin = (GButton)this.GetChildAt(11);
			m_btnMax = (GButton)this.GetChildAt(12);
			m_slider = (GSlider)this.GetChildAt(13);
			m_btnCompose = (GButton)this.GetChildAt(14);
			m_txtDes = (GTextField)this.GetChildAt(15);
		}
	}
}