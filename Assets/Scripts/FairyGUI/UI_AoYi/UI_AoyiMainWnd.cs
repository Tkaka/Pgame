/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiMainWnd : GComponent
	{
		public Controller m_c1;
		public GComponent m_commonTop;
		public GList m_petList;
		public GButton m_tabHigh;
		public GButton m_tabMiddle;
		public GButton m_tabPrimary;
		public GImage m_imgChujiRed;
		public GImage m_imgZhongjiRed;
		public GImage m_imgJiuJiRed;
		public GGraph m_modelPos;
		public GGraph m_modelToucher;
		public GLoader m_petTypeLoader;
		public GTextField m_petNameLabel;
		public GButton m_btnRongLian;
		public GButton m_btnGetStone;
		public GList m_stonePropertyList;
		public GList m_skillPropertyList;
		public GTextField m_txtKillName;
		public GTextField m_txtPropertyName;
		public UI_ayIocnList m_iconList;
		public GButton m_btnOneKeyUnEquip;
		public GButton m_btnOneKeyStrength;
		public GButton m_btnOneKeyPut;
		public GButton m_btnSkill;
		public GList m_StoneGridList;
		public GButton m_btnChange;
		public GImage m_imgGetRewardRed;

		public const string URL = "ui://vexa0xrycpnr0";

		public static UI_AoyiMainWnd CreateInstance()
		{
			return (UI_AoyiMainWnd)UIPackage.CreateObject("UI_AoYi","AoyiMainWnd");
		}

		public UI_AoyiMainWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
			m_commonTop = (GComponent)this.GetChildAt(1);
			m_petList = (GList)this.GetChildAt(4);
			m_tabHigh = (GButton)this.GetChildAt(6);
			m_tabMiddle = (GButton)this.GetChildAt(7);
			m_tabPrimary = (GButton)this.GetChildAt(8);
			m_imgChujiRed = (GImage)this.GetChildAt(9);
			m_imgZhongjiRed = (GImage)this.GetChildAt(10);
			m_imgJiuJiRed = (GImage)this.GetChildAt(11);
			m_modelPos = (GGraph)this.GetChildAt(14);
			m_modelToucher = (GGraph)this.GetChildAt(15);
			m_petTypeLoader = (GLoader)this.GetChildAt(17);
			m_petNameLabel = (GTextField)this.GetChildAt(18);
			m_btnRongLian = (GButton)this.GetChildAt(20);
			m_btnGetStone = (GButton)this.GetChildAt(21);
			m_stonePropertyList = (GList)this.GetChildAt(24);
			m_skillPropertyList = (GList)this.GetChildAt(27);
			m_txtKillName = (GTextField)this.GetChildAt(29);
			m_txtPropertyName = (GTextField)this.GetChildAt(30);
			m_iconList = (UI_ayIocnList)this.GetChildAt(31);
			m_btnOneKeyUnEquip = (GButton)this.GetChildAt(32);
			m_btnOneKeyStrength = (GButton)this.GetChildAt(33);
			m_btnOneKeyPut = (GButton)this.GetChildAt(34);
			m_btnSkill = (GButton)this.GetChildAt(35);
			m_StoneGridList = (GList)this.GetChildAt(37);
			m_btnChange = (GButton)this.GetChildAt(38);
			m_imgGetRewardRed = (GImage)this.GetChildAt(39);
		}
	}
}