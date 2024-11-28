/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_SeeOtherInfoWindow : GComponent
	{
		public GButton m_btnClose;
		public GLoader m_imgIcon;
		public GTextField m_txtName;
		public GTextField m_txtLevel;
		public GTextField m_txtRank;
		public GTextField m_txtVictoryCount;
		public GTextField m_txtXuanYan;
		public GTextField m_txtXianShou;
		public GTextField m_txtFightPower;
		public GList m_mainList;
		public GTextField m_txtComeFrom;
		public GComponent m_btnAddFriend;
		public GComponent m_btnSendMsg;

		public const string URL = "ui://3xs7lfyxehrw2a";

		public static UI_SeeOtherInfoWindow CreateInstance()
		{
			return (UI_SeeOtherInfoWindow)UIPackage.CreateObject("UI_Arena","SeeOtherInfoWindow");
		}

		public UI_SeeOtherInfoWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_imgIcon = (GLoader)this.GetChildAt(5);
			m_txtName = (GTextField)this.GetChildAt(6);
			m_txtLevel = (GTextField)this.GetChildAt(11);
			m_txtRank = (GTextField)this.GetChildAt(12);
			m_txtVictoryCount = (GTextField)this.GetChildAt(13);
			m_txtXuanYan = (GTextField)this.GetChildAt(14);
			m_txtXianShou = (GTextField)this.GetChildAt(16);
			m_txtFightPower = (GTextField)this.GetChildAt(18);
			m_mainList = (GList)this.GetChildAt(20);
			m_txtComeFrom = (GTextField)this.GetChildAt(21);
			m_btnAddFriend = (GComponent)this.GetChildAt(22);
			m_btnSendMsg = (GComponent)this.GetChildAt(23);
		}
	}
}