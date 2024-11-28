/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiResolveWnd : GComponent
	{
		public GList m_mainList;
		public GButton m_btnClose;
		public GButton m_btnFastSelect;
		public GButton m_btnOk;
		public GTextField m_txtGetDes;
		public GTextField m_txtSelectNum;

		public const string URL = "ui://vexa0xrydys1x";

		public static UI_AoyiResolveWnd CreateInstance()
		{
			return (UI_AoyiResolveWnd)UIPackage.CreateObject("UI_AoYi","AoyiResolveWnd");
		}

		public UI_AoyiResolveWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mainList = (GList)this.GetChildAt(6);
			m_btnClose = (GButton)this.GetChildAt(7);
			m_btnFastSelect = (GButton)this.GetChildAt(8);
			m_btnOk = (GButton)this.GetChildAt(9);
			m_txtGetDes = (GTextField)this.GetChildAt(10);
			m_txtSelectNum = (GTextField)this.GetChildAt(11);
		}
	}
}