/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_XunLianWei : GComponent
	{
		public GTextField m_jiesuotiaojian;
		public GGroup m_WeiKaiQi;
		public GButton m_JiaSuBtn;
		public GGraph m_muzhuang;
		public GTextField m_JiaGe;
		public GButton m_KaiTongBtn;
		public GGroup m_kaitong;
		public GGraph m_GengHuanBtn;
		public GGraph m_pet;
		public GTextField m_name;
		public GTextField m_level;
		public UI_GD_JingYanJinDu m_jinyan;
		public GTextField m_xianshi;
		public GGroup m_QiPaoYuYan;
		public GTextField m_jingyanhuode;
		public GGroup m_JingYanHuoDe;
		public GGroup m_YiKaiQi;
		public Transition m_huodeDongXiao;

		public const string URL = "ui://wwlsouxzk46r3";

		public static UI_GD_XunLianWei CreateInstance()
		{
			return (UI_GD_XunLianWei)UIPackage.CreateObject("UI_GuildDrill","GD_XunLianWei");
		}

		public UI_GD_XunLianWei()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_jiesuotiaojian = (GTextField)this.GetChildAt(2);
			m_WeiKaiQi = (GGroup)this.GetChildAt(3);
			m_JiaSuBtn = (GButton)this.GetChildAt(4);
			m_muzhuang = (GGraph)this.GetChildAt(5);
			m_JiaGe = (GTextField)this.GetChildAt(6);
			m_KaiTongBtn = (GButton)this.GetChildAt(7);
			m_kaitong = (GGroup)this.GetChildAt(8);
			m_GengHuanBtn = (GGraph)this.GetChildAt(9);
			m_pet = (GGraph)this.GetChildAt(10);
			m_name = (GTextField)this.GetChildAt(11);
			m_level = (GTextField)this.GetChildAt(12);
			m_jinyan = (UI_GD_JingYanJinDu)this.GetChildAt(13);
			m_xianshi = (GTextField)this.GetChildAt(15);
			m_QiPaoYuYan = (GGroup)this.GetChildAt(16);
			m_jingyanhuode = (GTextField)this.GetChildAt(18);
			m_JingYanHuoDe = (GGroup)this.GetChildAt(19);
			m_YiKaiQi = (GGroup)this.GetChildAt(20);
			m_huodeDongXiao = this.GetTransitionAt(0);
		}
	}
}