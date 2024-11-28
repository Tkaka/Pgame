/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Friend
{
	public partial class UI_FriendGiftWnd : GComponent
	{
		public GButton m_btnClose;
		public GComponent m_btnLeft;
		public GComponent m_btnRight;
		public GTextField m_txtPage;

		public const string URL = "ui://tvm1q5ekqkni1v";

		public static UI_FriendGiftWnd CreateInstance()
		{
			return (UI_FriendGiftWnd)UIPackage.CreateObject("UI_Friend","FriendGiftWnd");
		}

		public UI_FriendGiftWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(3);
			m_btnLeft = (GComponent)this.GetChildAt(4);
			m_btnRight = (GComponent)this.GetChildAt(5);
			m_txtPage = (GTextField)this.GetChildAt(6);
		}
	}
}