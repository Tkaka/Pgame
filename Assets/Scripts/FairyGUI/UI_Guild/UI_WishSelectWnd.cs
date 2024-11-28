/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_WishSelectWnd : GComponent
	{
		public Controller m_tabControl;
		public GButton m_btnClose;
		public GList m_itemList;
		public GButton m_imgIcon;
		public GTextField m_txtNum;
		public UI_btnOk m_btnOk;

		public const string URL = "ui://oe7ras64ts1sb3v";

		public static UI_WishSelectWnd CreateInstance()
		{
			return (UI_WishSelectWnd)UIPackage.CreateObject("UI_Guild","WishSelectWnd");
		}

		public UI_WishSelectWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tabControl = this.GetControllerAt(0);
			m_btnClose = (GButton)this.GetChildAt(1);
			m_itemList = (GList)this.GetChildAt(13);
			m_imgIcon = (GButton)this.GetChildAt(14);
			m_txtNum = (GTextField)this.GetChildAt(16);
			m_btnOk = (UI_btnOk)this.GetChildAt(17);
		}
	}
}