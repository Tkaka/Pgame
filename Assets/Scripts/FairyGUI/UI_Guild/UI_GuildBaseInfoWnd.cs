/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_GuildBaseInfoWnd : GComponent
	{
		public GLoader m_imgHuiZhang;
		public GComponent m_btnChangeHuiZhang;
		public GTextField m_txtGuildName;
		public GTextField m_txtGuildLevel;
		public GComponent m_btnChangeName;
		public UI_btnQuit m_btnQuit;
		public UI_Dropdown m_cbxType;
		public GTextField m_txtGuildType;
		public UI_sendMail m_btnSendMail;
		public UI_btnShuoMing m_btnShuoMing;
		public GList m_memberList;
		public GTextField m_txtChairMan;
		public GTextField m_txtPeopleNum;
		public GTextField m_txtChairManNum;
		public GTextField m_txtGuildNum;
		public GTextField m_txtGuildRank;
		public GTextField m_txtViceChairMan;
		public GTextField m_txtEliteNum;

		public const string URL = "ui://oe7ras64fde91z";

		public static UI_GuildBaseInfoWnd CreateInstance()
		{
			return (UI_GuildBaseInfoWnd)UIPackage.CreateObject("UI_Guild","GuildBaseInfoWnd");
		}

		public UI_GuildBaseInfoWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgHuiZhang = (GLoader)this.GetChildAt(0);
			m_btnChangeHuiZhang = (GComponent)this.GetChildAt(1);
			m_txtGuildName = (GTextField)this.GetChildAt(3);
			m_txtGuildLevel = (GTextField)this.GetChildAt(4);
			m_btnChangeName = (GComponent)this.GetChildAt(5);
			m_btnQuit = (UI_btnQuit)this.GetChildAt(7);
			m_cbxType = (UI_Dropdown)this.GetChildAt(8);
			m_txtGuildType = (GTextField)this.GetChildAt(9);
			m_btnSendMail = (UI_sendMail)this.GetChildAt(10);
			m_btnShuoMing = (UI_btnShuoMing)this.GetChildAt(11);
			m_memberList = (GList)this.GetChildAt(14);
			m_txtChairMan = (GTextField)this.GetChildAt(22);
			m_txtPeopleNum = (GTextField)this.GetChildAt(23);
			m_txtChairManNum = (GTextField)this.GetChildAt(24);
			m_txtGuildNum = (GTextField)this.GetChildAt(25);
			m_txtGuildRank = (GTextField)this.GetChildAt(26);
			m_txtViceChairMan = (GTextField)this.GetChildAt(27);
			m_txtEliteNum = (GTextField)this.GetChildAt(28);
		}
	}
}