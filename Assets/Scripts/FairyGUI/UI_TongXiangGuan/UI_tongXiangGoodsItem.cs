/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_tongXiangGoodsItem : GComponent
	{
		public GTextField m_rankName;
		public GGraph m_modelPos;
		public GTextField m_atkLabel;
		public GTextField m_defLabel;
		public GTextField m_hpLabel;
		public GButton m_switchGoodsBtn;
		public GImage m_useTip;
		public GButton m_buyBtn;
		public GTextField m_diamondNum;
		public GGroup m_diamondGroup;
		public GTextField m_goldNum;
		public GGroup m_goldGroup;
		public GGroup m_buyGroup;
		public GImage m_switchSuccessIcon;
		public GTextField m_geDouJiaTipLabel;
		public GGroup m_geDouJiaTipGroup;

		public const string URL = "ui://ansp6fm5x9sqm";

		public static UI_tongXiangGoodsItem CreateInstance()
		{
			return (UI_tongXiangGoodsItem)UIPackage.CreateObject("UI_TongXiangGuan","tongXiangGoodsItem");
		}

		public UI_tongXiangGoodsItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rankName = (GTextField)this.GetChildAt(1);
			m_modelPos = (GGraph)this.GetChildAt(2);
			m_atkLabel = (GTextField)this.GetChildAt(4);
			m_defLabel = (GTextField)this.GetChildAt(6);
			m_hpLabel = (GTextField)this.GetChildAt(8);
			m_switchGoodsBtn = (GButton)this.GetChildAt(9);
			m_useTip = (GImage)this.GetChildAt(10);
			m_buyBtn = (GButton)this.GetChildAt(11);
			m_diamondNum = (GTextField)this.GetChildAt(13);
			m_diamondGroup = (GGroup)this.GetChildAt(14);
			m_goldNum = (GTextField)this.GetChildAt(16);
			m_goldGroup = (GGroup)this.GetChildAt(17);
			m_buyGroup = (GGroup)this.GetChildAt(18);
			m_switchSuccessIcon = (GImage)this.GetChildAt(19);
			m_geDouJiaTipLabel = (GTextField)this.GetChildAt(21);
			m_geDouJiaTipGroup = (GGroup)this.GetChildAt(22);
		}
	}
}