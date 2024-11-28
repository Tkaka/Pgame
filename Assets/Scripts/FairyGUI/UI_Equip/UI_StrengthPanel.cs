/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_StrengthPanel : GComponent
	{
		public GList m_itemList;
		public GButton m_normalShengPingBtn;
		public GTextField m_normalSPGoldLabel;
		public GGroup m_normalEnoughLvGroup;
		public GTextField m_normalUnEnoughLvTip;
		public GGroup m_normalShengPingGroup;
		public GLoader m_haveCoinLoader;
		public GTextField m_haveCoinNumLabel;
		public GTextField m_tipLabel;
		public GTextField m_specialUnEnoughLvTip;
		public GButton m_specialShengPingBtn;
		public GLoader m_useCoinLoader;
		public GTextField m_useCoinNum;
		public GTextField m_SpecialSPGoldNum;
		public GGroup m_specialEnoughLvGroup;
		public GGroup m_specialShengPingGroup;
		public GGroup m_shengPingGroup;
		public GButton m_normalQuickBtn;
		public GButton m_normalKeyBtn;
		public GTextField m_normalUpgradeGoldLabel;
		public GButton m_normalUpgradeBtn;
		public GGroup m_normalEquipUpgrad;
		public GProgressBar m_expPrograssBar;
		public GList m_SpecialExpList;
		public GButton m_specialQucikBtn;
		public GTextField m_useNumLabel;
		public GTextField m_specialEqupExpTip;
		public GGroup m_specialEquipUpgrade;
		public GGroup m_upgradeGroup;
		public GGraph m_effectHolder;
		public GLoader m_equipImg;
		public GTextField m_nameLabel;
		public GList m_atrributeList;
		public GTextField m_levelLabel;
		public GList m_starList;
		public UI_sheng_pin_eff m_shengPingEff;
		public GGraph m_lvEffPos;
		public Transition m_levelAnim;
		public Transition m_equipModelAnim;

		public const string URL = "ui://8u3gv94nvds0r";

		public static UI_StrengthPanel CreateInstance()
		{
			return (UI_StrengthPanel)UIPackage.CreateObject("UI_Equip","StrengthPanel");
		}

		public UI_StrengthPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_itemList = (GList)this.GetChildAt(1);
			m_normalShengPingBtn = (GButton)this.GetChildAt(2);
			m_normalSPGoldLabel = (GTextField)this.GetChildAt(4);
			m_normalEnoughLvGroup = (GGroup)this.GetChildAt(5);
			m_normalUnEnoughLvTip = (GTextField)this.GetChildAt(6);
			m_normalShengPingGroup = (GGroup)this.GetChildAt(7);
			m_haveCoinLoader = (GLoader)this.GetChildAt(9);
			m_haveCoinNumLabel = (GTextField)this.GetChildAt(10);
			m_tipLabel = (GTextField)this.GetChildAt(11);
			m_specialUnEnoughLvTip = (GTextField)this.GetChildAt(12);
			m_specialShengPingBtn = (GButton)this.GetChildAt(13);
			m_useCoinLoader = (GLoader)this.GetChildAt(14);
			m_useCoinNum = (GTextField)this.GetChildAt(16);
			m_SpecialSPGoldNum = (GTextField)this.GetChildAt(17);
			m_specialEnoughLvGroup = (GGroup)this.GetChildAt(18);
			m_specialShengPingGroup = (GGroup)this.GetChildAt(19);
			m_shengPingGroup = (GGroup)this.GetChildAt(20);
			m_normalQuickBtn = (GButton)this.GetChildAt(23);
			m_normalKeyBtn = (GButton)this.GetChildAt(24);
			m_normalUpgradeGoldLabel = (GTextField)this.GetChildAt(26);
			m_normalUpgradeBtn = (GButton)this.GetChildAt(27);
			m_normalEquipUpgrad = (GGroup)this.GetChildAt(28);
			m_expPrograssBar = (GProgressBar)this.GetChildAt(29);
			m_SpecialExpList = (GList)this.GetChildAt(31);
			m_specialQucikBtn = (GButton)this.GetChildAt(33);
			m_useNumLabel = (GTextField)this.GetChildAt(34);
			m_specialEqupExpTip = (GTextField)this.GetChildAt(35);
			m_specialEquipUpgrade = (GGroup)this.GetChildAt(36);
			m_upgradeGroup = (GGroup)this.GetChildAt(37);
			m_effectHolder = (GGraph)this.GetChildAt(38);
			m_equipImg = (GLoader)this.GetChildAt(39);
			m_nameLabel = (GTextField)this.GetChildAt(40);
			m_atrributeList = (GList)this.GetChildAt(43);
			m_levelLabel = (GTextField)this.GetChildAt(44);
			m_starList = (GList)this.GetChildAt(45);
			m_shengPingEff = (UI_sheng_pin_eff)this.GetChildAt(46);
			m_lvEffPos = (GGraph)this.GetChildAt(47);
			m_levelAnim = this.GetTransitionAt(0);
			m_equipModelAnim = this.GetTransitionAt(1);
		}
	}
}