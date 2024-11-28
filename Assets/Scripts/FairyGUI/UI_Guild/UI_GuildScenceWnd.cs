/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_GuildScenceWnd : GComponent
	{
		public UI_windowCloseBtn m_btnClose;
		public UI_btnHall m_btnGuildHall;
		public UI_btnReward m_btnReward;
		public UI_objNotice m_objNotice;
		public GComponent m_btnGuildShop;
		public UI_btnWish m_btnWish;
		public GComponent m_XunLianSuoBtn;
		public UI_btnDonate m_btnDonate;
		public UI_btnGuildBoss m_guildBossBtn;
		public UI_HongBaoBtn m_hongbaoBtn;

		public const string URL = "ui://oe7ras64qbwu1";

		public static UI_GuildScenceWnd CreateInstance()
		{
			return (UI_GuildScenceWnd)UIPackage.CreateObject("UI_Guild","GuildScenceWnd");
		}

		public UI_GuildScenceWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(0);
			m_btnGuildHall = (UI_btnHall)this.GetChildAt(1);
			m_btnReward = (UI_btnReward)this.GetChildAt(2);
			m_objNotice = (UI_objNotice)this.GetChildAt(3);
			m_btnGuildShop = (GComponent)this.GetChildAt(4);
			m_btnWish = (UI_btnWish)this.GetChildAt(5);
			m_XunLianSuoBtn = (GComponent)this.GetChildAt(6);
			m_btnDonate = (UI_btnDonate)this.GetChildAt(7);
			m_guildBossBtn = (UI_btnGuildBoss)this.GetChildAt(8);
			m_hongbaoBtn = (UI_HongBaoBtn)this.GetChildAt(9);
		}
	}
}