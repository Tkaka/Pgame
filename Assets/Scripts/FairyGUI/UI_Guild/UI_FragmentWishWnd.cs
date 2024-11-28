/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_FragmentWishWnd : GComponent
	{
		public GButton m_btnClose;
		public UI_objWishPlayerCell m_objRole;
		public GList m_playerList;
		public GTextField m_txtNotice;
		public GTextField m_;
		public GTextField m_txtCount;

		public const string URL = "ui://oe7ras64vmh3b3l";

		public static UI_FragmentWishWnd CreateInstance()
		{
			return (UI_FragmentWishWnd)UIPackage.CreateObject("UI_Guild","FragmentWishWnd");
		}

		public UI_FragmentWishWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_objRole = (UI_objWishPlayerCell)this.GetChildAt(2);
			m_playerList = (GList)this.GetChildAt(3);
			m_txtNotice = (GTextField)this.GetChildAt(8);
			m_ = (GTextField)this.GetChildAt(9);
			m_txtCount = (GTextField)this.GetChildAt(10);
		}
	}
}