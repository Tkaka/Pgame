/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_BaQiangListItem : GComponent
	{
		public GTextField m_ChangCi;
		public GTextField m_BeiZhan;
		public GButton m_GuanZhanBtn;
		public GButton m_luxiangBtn;
		public GButton m_fenxiangBtn;
		public GList m_my;
		public GList m_ther;
		public GLoader m_wodezhankuang;
		public GLoader m_duishouzhankuang;
		public GTextField m_daojishi;
		public GTextField m_duizhanIcon;
		public GTextField m_my_name;
		public GTextField m_my_level;
		public GTextField m_ther_level;
		public GTextField m_ther_name;
		public GGraph m_BaoXiangBtn;
		public GGroup m_BaoXiang;

		public const string URL = "ui://yjvxfmwodshf1w";

		public static UI_SH_BaQiangListItem CreateInstance()
		{
			return (UI_SH_BaQiangListItem)UIPackage.CreateObject("UI_StriveHegemong","SH_BaQiangListItem");
		}

		public UI_SH_BaQiangListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ChangCi = (GTextField)this.GetChildAt(1);
			m_BeiZhan = (GTextField)this.GetChildAt(2);
			m_GuanZhanBtn = (GButton)this.GetChildAt(3);
			m_luxiangBtn = (GButton)this.GetChildAt(4);
			m_fenxiangBtn = (GButton)this.GetChildAt(5);
			m_my = (GList)this.GetChildAt(6);
			m_ther = (GList)this.GetChildAt(7);
			m_wodezhankuang = (GLoader)this.GetChildAt(8);
			m_duishouzhankuang = (GLoader)this.GetChildAt(9);
			m_daojishi = (GTextField)this.GetChildAt(10);
			m_duizhanIcon = (GTextField)this.GetChildAt(11);
			m_my_name = (GTextField)this.GetChildAt(12);
			m_my_level = (GTextField)this.GetChildAt(14);
			m_ther_level = (GTextField)this.GetChildAt(16);
			m_ther_name = (GTextField)this.GetChildAt(17);
			m_BaoXiangBtn = (GGraph)this.GetChildAt(19);
			m_BaoXiang = (GGroup)this.GetChildAt(20);
		}
	}
}