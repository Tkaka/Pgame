/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiOnekeyStrengthWnd : GComponent
	{
		public GButton m_btnClose;
		public GList m_StoneGridList;
		public GList m_propertyList;
		public GTextField m_txtLevelDes;
		public GButton m_btnOneKey;
		public GSlider m_slider;
		public GTextField m_txtCoinNum;
		public GTextField m_txtAyNum;

		public const string URL = "ui://vexa0xrygc7j1g";

		public static UI_AoyiOnekeyStrengthWnd CreateInstance()
		{
			return (UI_AoyiOnekeyStrengthWnd)UIPackage.CreateObject("UI_AoYi","AoyiOnekeyStrengthWnd");
		}

		public UI_AoyiOnekeyStrengthWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(5);
			m_StoneGridList = (GList)this.GetChildAt(7);
			m_propertyList = (GList)this.GetChildAt(9);
			m_txtLevelDes = (GTextField)this.GetChildAt(13);
			m_btnOneKey = (GButton)this.GetChildAt(14);
			m_slider = (GSlider)this.GetChildAt(15);
			m_txtCoinNum = (GTextField)this.GetChildAt(17);
			m_txtAyNum = (GTextField)this.GetChildAt(21);
		}
	}
}