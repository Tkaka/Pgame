/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_XZ_XiaZhuListItem : GComponent
	{
		public GLoader m_mingciIcon;
		public GTextField m_paiming;
		public GLoader m_beijing;
		public GLoader m_TouXiang;
		public GTextField m_name;
		public GTextField m_dengji;
		public GTextField m_PeiLv;
		public GButton m_GaoJiXiaZhu;
		public GButton m_PuTongXiaZhu;
		public GTextField m_putongbenjin;
		public GTextField m_gaojibenjin;
		public GGroup m_WiXiaZhu;
		public GTextField m_xiazhujine;
		public GGroup m_YiXiaZhu;

		public const string URL = "ui://yjvxfmwon7xzw";

		public static UI_SH_XZ_XiaZhuListItem CreateInstance()
		{
			return (UI_SH_XZ_XiaZhuListItem)UIPackage.CreateObject("UI_StriveHegemong","SH_XZ_XiaZhuListItem");
		}

		public UI_SH_XZ_XiaZhuListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mingciIcon = (GLoader)this.GetChildAt(1);
			m_paiming = (GTextField)this.GetChildAt(2);
			m_beijing = (GLoader)this.GetChildAt(3);
			m_TouXiang = (GLoader)this.GetChildAt(4);
			m_name = (GTextField)this.GetChildAt(5);
			m_dengji = (GTextField)this.GetChildAt(6);
			m_PeiLv = (GTextField)this.GetChildAt(8);
			m_GaoJiXiaZhu = (GButton)this.GetChildAt(9);
			m_PuTongXiaZhu = (GButton)this.GetChildAt(10);
			m_putongbenjin = (GTextField)this.GetChildAt(11);
			m_gaojibenjin = (GTextField)this.GetChildAt(12);
			m_WiXiaZhu = (GGroup)this.GetChildAt(13);
			m_xiazhujine = (GTextField)this.GetChildAt(16);
			m_YiXiaZhu = (GGroup)this.GetChildAt(17);
		}
	}
}