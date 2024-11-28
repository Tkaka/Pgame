/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_WS_ListItem : GComponent
	{
		public GTextField m_changci;
		public GTextField m_DuiZhan;
		public UI_SH_HG_Figure m_ther;
		public GTextField m_beizhan;
		public UI_SH_HG_Figure m_my;
		public GButton m_luxiang;
		public GButton m_fenxiang;
		public GGroup m_HuiKan;
		public GTextField m_myName;
		public GTextField m_myLevel;
		public GTextField m_shangzhen;
		public GTextField m_therLevel;
		public GTextField m_therName;
		public GTextField m_ZhanDouZhong;
		public GLoader m_BaoXiang;
		public GGraph m_BaoXiangBtn;
		public GGroup m_JieShu;
		public GTextField m_daojishi;
		public GButton m_guanzhan;
		public GGraph m_myZhenRong;
		public GGraph m_therZhenRong;
		public GGroup m_YiKaiSai;

		public const string URL = "ui://yjvxfmwon7xzh";

		public static UI_SH_WS_ListItem CreateInstance()
		{
			return (UI_SH_WS_ListItem)UIPackage.CreateObject("UI_StriveHegemong","SH_WS_ListItem");
		}

		public UI_SH_WS_ListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_changci = (GTextField)this.GetChildAt(1);
			m_DuiZhan = (GTextField)this.GetChildAt(2);
			m_ther = (UI_SH_HG_Figure)this.GetChildAt(3);
			m_beizhan = (GTextField)this.GetChildAt(4);
			m_my = (UI_SH_HG_Figure)this.GetChildAt(5);
			m_luxiang = (GButton)this.GetChildAt(6);
			m_fenxiang = (GButton)this.GetChildAt(7);
			m_HuiKan = (GGroup)this.GetChildAt(8);
			m_myName = (GTextField)this.GetChildAt(9);
			m_myLevel = (GTextField)this.GetChildAt(11);
			m_shangzhen = (GTextField)this.GetChildAt(12);
			m_therLevel = (GTextField)this.GetChildAt(13);
			m_therName = (GTextField)this.GetChildAt(14);
			m_ZhanDouZhong = (GTextField)this.GetChildAt(15);
			m_BaoXiang = (GLoader)this.GetChildAt(16);
			m_BaoXiangBtn = (GGraph)this.GetChildAt(17);
			m_JieShu = (GGroup)this.GetChildAt(18);
			m_daojishi = (GTextField)this.GetChildAt(19);
			m_guanzhan = (GButton)this.GetChildAt(20);
			m_myZhenRong = (GGraph)this.GetChildAt(21);
			m_therZhenRong = (GGraph)this.GetChildAt(22);
			m_YiKaiSai = (GGroup)this.GetChildAt(23);
		}
	}
}