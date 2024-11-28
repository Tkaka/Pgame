/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Friend
{
	public partial class UI_FriendInfoWnd : GComponent
	{
		public GButton m_btnClose;
		public UI_objTouXiang m_objIcon;
		public UI_objVip m_objVip;
		public GTextField m_txtName;
		public GTextField m_txtLevel;
		public GTextField m_txtGuild;
		public UI_btnLaHei m_btnLaHei;
		public UI_btnQieCuo m_btnQieCuo;
		public UI_btnSendMsg m_btnSendMsg;
		public UI_btnAddFriend m_btnAddFriend;

		public const string URL = "ui://tvm1q5ekqkni1y";

		public static UI_FriendInfoWnd CreateInstance()
		{
			return (UI_FriendInfoWnd)UIPackage.CreateObject("UI_Friend","FriendInfoWnd");
		}

		public UI_FriendInfoWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_objIcon = (UI_objTouXiang)this.GetChildAt(3);
			m_objVip = (UI_objVip)this.GetChildAt(4);
			m_txtName = (GTextField)this.GetChildAt(5);
			m_txtLevel = (GTextField)this.GetChildAt(6);
			m_txtGuild = (GTextField)this.GetChildAt(7);
			m_btnLaHei = (UI_btnLaHei)this.GetChildAt(8);
			m_btnQieCuo = (UI_btnQieCuo)this.GetChildAt(9);
			m_btnSendMsg = (UI_btnSendMsg)this.GetChildAt(10);
			m_btnAddFriend = (UI_btnAddFriend)this.GetChildAt(11);
		}
	}
}