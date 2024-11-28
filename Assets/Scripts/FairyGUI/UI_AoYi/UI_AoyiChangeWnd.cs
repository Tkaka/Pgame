/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiChangeWnd : GComponent
	{
		public GComponent m_commonTop;
		public GButton m_btnUnEquip;
		public UI_sortDropDown m_dropDown;
		public GButton m_btnPut;
		public GButton m_btnSkill;
		public GTextField m_txtAoyiName;
		public UI_ayIocnList m_iconList;
		public GList m_aoyiGroupList;
		public GTextField m_txtPage;
		public GButton m_btnRight;
		public GButton m_btnLeft;
		public GList m_aoyiBagList;

		public const string URL = "ui://vexa0xryh9lhg";

		public static UI_AoyiChangeWnd CreateInstance()
		{
			return (UI_AoyiChangeWnd)UIPackage.CreateObject("UI_AoYi","AoyiChangeWnd");
		}

		public UI_AoyiChangeWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_commonTop = (GComponent)this.GetChildAt(1);
			m_btnUnEquip = (GButton)this.GetChildAt(4);
			m_dropDown = (UI_sortDropDown)this.GetChildAt(5);
			m_btnPut = (GButton)this.GetChildAt(7);
			m_btnSkill = (GButton)this.GetChildAt(9);
			m_txtAoyiName = (GTextField)this.GetChildAt(12);
			m_iconList = (UI_ayIocnList)this.GetChildAt(14);
			m_aoyiGroupList = (GList)this.GetChildAt(16);
			m_txtPage = (GTextField)this.GetChildAt(19);
			m_btnRight = (GButton)this.GetChildAt(20);
			m_btnLeft = (GButton)this.GetChildAt(21);
			m_aoyiBagList = (GList)this.GetChildAt(22);
		}
	}
}