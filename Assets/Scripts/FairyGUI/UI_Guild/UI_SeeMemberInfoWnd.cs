/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_SeeMemberInfoWnd : GComponent
	{
		public GButton m_btnClose;
		public UI_headIcon m_headIocn;
		public GTextField m_txtName;
		public GTextField m_txtJob;
		public GTextField m_txtLevel;
		public UI_btnAddFriend m_btnAddFriend;
		public UI_btnSendMsg m_btnSendMsg;
		public GGroup m_btnGroup;
		public GList m_PetList;
		public UI_btnKickOut m_btnTiChu;
		public UI_btnChuanZhi m_btnChuanZhi;
		public UI_btnJiangZhi m_btnJiangZhi;
		public UI_btnShenZhi m_btnShenZhi;
		public GList m_btnList;
		public GGroup m_objSelect;
		public GGroup m_OperationGroup;
		public UI_btnOk m_btnOk;
		public GTextField m_txtTime;
		public GTextField m_txtContribution;
		public GTextField m_txtFightPower;

		public const string URL = "ui://oe7ras64fde928";

		public static UI_SeeMemberInfoWnd CreateInstance()
		{
			return (UI_SeeMemberInfoWnd)UIPackage.CreateObject("UI_Guild","SeeMemberInfoWnd");
		}

		public UI_SeeMemberInfoWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(2);
			m_headIocn = (UI_headIcon)this.GetChildAt(3);
			m_txtName = (GTextField)this.GetChildAt(4);
			m_txtJob = (GTextField)this.GetChildAt(8);
			m_txtLevel = (GTextField)this.GetChildAt(9);
			m_btnAddFriend = (UI_btnAddFriend)this.GetChildAt(10);
			m_btnSendMsg = (UI_btnSendMsg)this.GetChildAt(11);
			m_btnGroup = (GGroup)this.GetChildAt(12);
			m_PetList = (GList)this.GetChildAt(13);
			m_btnTiChu = (UI_btnKickOut)this.GetChildAt(14);
			m_btnChuanZhi = (UI_btnChuanZhi)this.GetChildAt(15);
			m_btnJiangZhi = (UI_btnJiangZhi)this.GetChildAt(16);
			m_btnShenZhi = (UI_btnShenZhi)this.GetChildAt(17);
			m_btnList = (GList)this.GetChildAt(19);
			m_objSelect = (GGroup)this.GetChildAt(20);
			m_OperationGroup = (GGroup)this.GetChildAt(21);
			m_btnOk = (UI_btnOk)this.GetChildAt(22);
			m_txtTime = (GTextField)this.GetChildAt(23);
			m_txtContribution = (GTextField)this.GetChildAt(24);
			m_txtFightPower = (GTextField)this.GetChildAt(25);
		}
	}
}