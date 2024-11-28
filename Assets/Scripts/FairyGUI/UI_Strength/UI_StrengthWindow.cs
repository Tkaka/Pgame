/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_StrengthWindow : GComponent
	{
		public Controller m_strengthCtrl;
		public UI_toggleGroup m_toggleGroup;
		public GGraph m_modelPos;
		public GGraph m_modelToucher;
		public GTextField m_zhanDouLiLabel;
		public GTextField m_atkLabel;
		public GTextField m_defLabel;
		public GTextField m_hpLabel;
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;
		public GLoader m_petTypeLoader;
		public GList m_petStarList;
		public GTextField m_petNameLabel;
		public UI_ZhanHunPanel m_zhanHunPanel;
		public UI_jiNeng m_jiNeng;
		public UI_JinHuaPanel m_jinhuaPanel;
		public UI_shengJi m_shengJi;
		public UI_shengPing m_shengPing;
		public GList m_strengthPetList;
		public GButton m_bottomSwitchRightBtn;
		public GButton m_bottomSwitchLeftBtn;
		public GComponent m_commonTop;
		public GGraph m_mask;

		public const string URL = "ui://qnd9tp35lfw10";

		public static UI_StrengthWindow CreateInstance()
		{
			return (UI_StrengthWindow)UIPackage.CreateObject("UI_Strength","StrengthWindow");
		}

		public UI_StrengthWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_strengthCtrl = this.GetControllerAt(0);
			m_toggleGroup = (UI_toggleGroup)this.GetChildAt(1);
			m_modelPos = (GGraph)this.GetChildAt(4);
			m_modelToucher = (GGraph)this.GetChildAt(5);
			m_zhanDouLiLabel = (GTextField)this.GetChildAt(10);
			m_atkLabel = (GTextField)this.GetChildAt(15);
			m_defLabel = (GTextField)this.GetChildAt(19);
			m_hpLabel = (GTextField)this.GetChildAt(23);
			m_switchLeftBtn = (GButton)this.GetChildAt(24);
			m_switchRightBtn = (GButton)this.GetChildAt(25);
			m_petTypeLoader = (GLoader)this.GetChildAt(27);
			m_petStarList = (GList)this.GetChildAt(28);
			m_petNameLabel = (GTextField)this.GetChildAt(29);
			m_zhanHunPanel = (UI_ZhanHunPanel)this.GetChildAt(31);
			m_jiNeng = (UI_jiNeng)this.GetChildAt(32);
			m_jinhuaPanel = (UI_JinHuaPanel)this.GetChildAt(33);
			m_shengJi = (UI_shengJi)this.GetChildAt(34);
			m_shengPing = (UI_shengPing)this.GetChildAt(35);
			m_strengthPetList = (GList)this.GetChildAt(36);
			m_bottomSwitchRightBtn = (GButton)this.GetChildAt(37);
			m_bottomSwitchLeftBtn = (GButton)this.GetChildAt(38);
			m_commonTop = (GComponent)this.GetChildAt(40);
			m_mask = (GGraph)this.GetChildAt(41);
		}
	}
}