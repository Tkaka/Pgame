/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_ShenQiMainWidow : GComponent
	{
		public Controller m_peiYangBtnCtrl;
		public GButton m_backBtn;
		public GButton m_introduceShenQiBtn;
		public GList m_shenQiList;
		public GButton m_switchRightBtn;
		public GButton m_switchLeftBtn;
		public GTextField m_nengYuanNumLabel;
		public GButton m_nengYuanAddBtn;
		public GGroup m_unlockAddGroup;
		public GList m_propertyList;
		public GTextField m_consumeNengYuanLabel;
		public GButton m_cancelBtn;
		public GButton m_saveBtn;
		public GGroup m_saveGroup;
		public GButton m_useOneTimeBtn;
		public GButton m_useTenTimeBtn;
		public GTextField m_oneTimeRemainLabel;
		public GTextField m_tenTimeRemainLabel;
		public GGroup m_peiYangGroup;
		public GTextField m_consumeGoldLabel;
		public GGroup m_consumeGoldGroup;
		public GTextField m_consumeDiamondLabel;
		public GGroup m_consumeDiamondGroup;
		public GGroup m_unlockGroup;
		public GGraph m_modelPos;
		public GTextField m_nameLabel;
		public GTextField m_jianJieLabel;
		public GGroup m_modelUnlockGroup;
		public GList m_tenResList;
		public GTextField m_tenNengYuanLabel;
		public GButton m_tenResAddBtn;
		public GButton m_openRecommendBtn;
		public GButton m_closeTenResBtn;
		public GGroup m_tenCultureResGroup;
		public GList m_unlockCoditionList;
		public GButton m_unlockBtn;
		public GGroup m_lockGroup;

		public const string URL = "ui://bi2nkn43fd9i0";

		public static UI_ShenQiMainWidow CreateInstance()
		{
			return (UI_ShenQiMainWidow)UIPackage.CreateObject("UI_ShenQi","ShenQiMainWidow");
		}

		public UI_ShenQiMainWidow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_peiYangBtnCtrl = this.GetControllerAt(0);
			m_backBtn = (GButton)this.GetChildAt(2);
			m_introduceShenQiBtn = (GButton)this.GetChildAt(3);
			m_shenQiList = (GList)this.GetChildAt(5);
			m_switchRightBtn = (GButton)this.GetChildAt(6);
			m_switchLeftBtn = (GButton)this.GetChildAt(7);
			m_nengYuanNumLabel = (GTextField)this.GetChildAt(11);
			m_nengYuanAddBtn = (GButton)this.GetChildAt(12);
			m_unlockAddGroup = (GGroup)this.GetChildAt(13);
			m_propertyList = (GList)this.GetChildAt(16);
			m_consumeNengYuanLabel = (GTextField)this.GetChildAt(29);
			m_cancelBtn = (GButton)this.GetChildAt(30);
			m_saveBtn = (GButton)this.GetChildAt(31);
			m_saveGroup = (GGroup)this.GetChildAt(32);
			m_useOneTimeBtn = (GButton)this.GetChildAt(33);
			m_useTenTimeBtn = (GButton)this.GetChildAt(34);
			m_oneTimeRemainLabel = (GTextField)this.GetChildAt(36);
			m_tenTimeRemainLabel = (GTextField)this.GetChildAt(38);
			m_peiYangGroup = (GGroup)this.GetChildAt(39);
			m_consumeGoldLabel = (GTextField)this.GetChildAt(41);
			m_consumeGoldGroup = (GGroup)this.GetChildAt(42);
			m_consumeDiamondLabel = (GTextField)this.GetChildAt(44);
			m_consumeDiamondGroup = (GGroup)this.GetChildAt(45);
			m_unlockGroup = (GGroup)this.GetChildAt(46);
			m_modelPos = (GGraph)this.GetChildAt(47);
			m_nameLabel = (GTextField)this.GetChildAt(48);
			m_jianJieLabel = (GTextField)this.GetChildAt(49);
			m_modelUnlockGroup = (GGroup)this.GetChildAt(56);
			m_tenResList = (GList)this.GetChildAt(61);
			m_tenNengYuanLabel = (GTextField)this.GetChildAt(65);
			m_tenResAddBtn = (GButton)this.GetChildAt(66);
			m_openRecommendBtn = (GButton)this.GetChildAt(68);
			m_closeTenResBtn = (GButton)this.GetChildAt(69);
			m_tenCultureResGroup = (GGroup)this.GetChildAt(70);
			m_unlockCoditionList = (GList)this.GetChildAt(72);
			m_unlockBtn = (GButton)this.GetChildAt(74);
			m_lockGroup = (GGroup)this.GetChildAt(75);
		}
	}
}