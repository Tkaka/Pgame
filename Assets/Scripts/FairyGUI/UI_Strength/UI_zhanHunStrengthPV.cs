/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_zhanHunStrengthPV : GComponent
	{
		public GLoader m_iconLoader;
		public GTextField m_nameLabel;
		public GTextField m_lvLabel;
		public GTextField m_descriptLabel;
		public GProgressBar m_tempProBar;
		public GProgressBar m_realProBar;
		public GButton m_closeBtn;
		public GTextField m_tempLvLabel;
		public GGroup m_previewLvGroup;
		public GTextField m_fullLabel;
		public GGroup m_fullGroup;
		public GTextField m_proBarValueLabel;
		public GList m_zhanHunCaiLiaoList;
		public GTextField m_itemNameLabel;
		public GTextField m_addExpLabel;
		public GButton m_addBtn;
		public GButton m_reduceBtn;
		public GSlider m_materialSlider;
		public GTextField m_addNumLabel;
		public GTextField m_unAddTipLabel;
		public GButton m_strengthBtn;
		public GButton m_zuanShiStrengthBtn;
		public GTextField m_goldLabel;
		public GGroup m_addCaiLiaoGroup;
		public GGroup m_unfullGroup;

		public const string URL = "ui://qnd9tp35lxvz4o";

		public static UI_zhanHunStrengthPV CreateInstance()
		{
			return (UI_zhanHunStrengthPV)UIPackage.CreateObject("UI_Strength","zhanHunStrengthPV");
		}

		public UI_zhanHunStrengthPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_iconLoader = (GLoader)this.GetChildAt(6);
			m_nameLabel = (GTextField)this.GetChildAt(7);
			m_lvLabel = (GTextField)this.GetChildAt(9);
			m_descriptLabel = (GTextField)this.GetChildAt(10);
			m_tempProBar = (GProgressBar)this.GetChildAt(11);
			m_realProBar = (GProgressBar)this.GetChildAt(12);
			m_closeBtn = (GButton)this.GetChildAt(13);
			m_tempLvLabel = (GTextField)this.GetChildAt(16);
			m_previewLvGroup = (GGroup)this.GetChildAt(17);
			m_fullLabel = (GTextField)this.GetChildAt(19);
			m_fullGroup = (GGroup)this.GetChildAt(21);
			m_proBarValueLabel = (GTextField)this.GetChildAt(24);
			m_zhanHunCaiLiaoList = (GList)this.GetChildAt(25);
			m_itemNameLabel = (GTextField)this.GetChildAt(26);
			m_addExpLabel = (GTextField)this.GetChildAt(28);
			m_addBtn = (GButton)this.GetChildAt(29);
			m_reduceBtn = (GButton)this.GetChildAt(30);
			m_materialSlider = (GSlider)this.GetChildAt(31);
			m_addNumLabel = (GTextField)this.GetChildAt(33);
			m_unAddTipLabel = (GTextField)this.GetChildAt(35);
			m_strengthBtn = (GButton)this.GetChildAt(36);
			m_zuanShiStrengthBtn = (GButton)this.GetChildAt(37);
			m_goldLabel = (GTextField)this.GetChildAt(40);
			m_addCaiLiaoGroup = (GGroup)this.GetChildAt(41);
			m_unfullGroup = (GGroup)this.GetChildAt(42);
		}
	}
}