/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_ZhenRongWindow : GComponent
	{
		public GTextField m_petZhanDouLi;
		public GTextField m_zizhi_num;
		public GTextField m_dengji_num;
		public GTextField m_gongji_num;
		public GTextField m_fangyu_num;
		public GTextField m_shengming_num;
		public GTextField m_jiBianText;
		public GGraph m_xiangQingToucher;
		public GLoader m_typeLoader;
		public GList m_starList;
		public GButton m_jinhualian;
		public GTextField m_nameLabel;
		public UI_zhenRongPetItem m_petItem0;
		public UI_zhenRongPetItem m_petItem1;
		public UI_zhenRongPetItem m_petItem2;
		public UI_zhenRongPetItem m_petItem3;
		public UI_zhenRongPetItem m_petItem4;
		public UI_zhenRongPetItem m_petItem5;
		public GTextField m_teamZhanDouLi;
		public GButton m_buZhenBtn;
		public GButton m_ZhuangBeiBtn;
		public GButton m_genHuanBtn;
		public GButton m_PeiYuBtn;
		public GGraph m_holder;
		public GGraph m_toucher;
		public GComponent m_equipItem2;
		public GComponent m_equipItem4;
		public GComponent m_equipItem1;
		public GComponent m_equipItem3;
		public GComponent m_equipItem0;
		public GComponent m_equipItem5;
		public GComponent m_commonTop;

		public const string URL = "ui://z0csav43k9p50";

		public static UI_ZhenRongWindow CreateInstance()
		{
			return (UI_ZhenRongWindow)UIPackage.CreateObject("UI_BuZhen","ZhenRongWindow");
		}

		public UI_ZhenRongWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_petZhanDouLi = (GTextField)this.GetChildAt(8);
			m_zizhi_num = (GTextField)this.GetChildAt(10);
			m_dengji_num = (GTextField)this.GetChildAt(12);
			m_gongji_num = (GTextField)this.GetChildAt(14);
			m_fangyu_num = (GTextField)this.GetChildAt(16);
			m_shengming_num = (GTextField)this.GetChildAt(19);
			m_jiBianText = (GTextField)this.GetChildAt(21);
			m_xiangQingToucher = (GGraph)this.GetChildAt(22);
			m_typeLoader = (GLoader)this.GetChildAt(24);
			m_starList = (GList)this.GetChildAt(25);
			m_jinhualian = (GButton)this.GetChildAt(28);
			m_nameLabel = (GTextField)this.GetChildAt(30);
			m_petItem0 = (UI_zhenRongPetItem)this.GetChildAt(31);
			m_petItem1 = (UI_zhenRongPetItem)this.GetChildAt(32);
			m_petItem2 = (UI_zhenRongPetItem)this.GetChildAt(33);
			m_petItem3 = (UI_zhenRongPetItem)this.GetChildAt(34);
			m_petItem4 = (UI_zhenRongPetItem)this.GetChildAt(35);
			m_petItem5 = (UI_zhenRongPetItem)this.GetChildAt(36);
			m_teamZhanDouLi = (GTextField)this.GetChildAt(38);
			m_buZhenBtn = (GButton)this.GetChildAt(39);
			m_ZhuangBeiBtn = (GButton)this.GetChildAt(40);
			m_genHuanBtn = (GButton)this.GetChildAt(41);
			m_PeiYuBtn = (GButton)this.GetChildAt(42);
			m_holder = (GGraph)this.GetChildAt(44);
			m_toucher = (GGraph)this.GetChildAt(45);
			m_equipItem2 = (GComponent)this.GetChildAt(46);
			m_equipItem4 = (GComponent)this.GetChildAt(47);
			m_equipItem1 = (GComponent)this.GetChildAt(48);
			m_equipItem3 = (GComponent)this.GetChildAt(49);
			m_equipItem0 = (GComponent)this.GetChildAt(50);
			m_equipItem5 = (GComponent)this.GetChildAt(51);
			m_commonTop = (GComponent)this.GetChildAt(53);
		}
	}
}