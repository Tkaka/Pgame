/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiStrengthWnd : GComponent
	{
		public GButton m_btnClose;
		public UI_AoyiCommonItem m_Icon;
		public GTextField m_txtStoneName;
		public UI_ayIocnList m_dicList;
		public GTextField m_objFull;
		public GTextField m_txtLevel;
		public GProgressBar m_progressBar;
		public GTextField m_txtProgressNum;
		public GList m_propertyList;
		public GTextField m_txtLevelUpDes;
		public GButton m_btnOneKeyStrength;
		public GButton m_btnLevelUp;
		public GTextField m_txtCosumeNum;
		public GGroup m_groupLevelUp;
		public GGroup m_minLevelGroup;
		public UI_AoyiCommonItem m_curLevelIcon;
		public UI_AoyiCommonItem m_nextLevelIcon;
		public GTextField m_txtCurLevelName;
		public GTextField m_txtNextLevelName;
		public GTextField m_;
		public GTextField m__2;
		public GTextField m_txtCoinNum;
		public GButton m_btnBreak;
		public GGroup m_breakGroup;

		public const string URL = "ui://vexa0xrygc7j17";

		public static UI_AoyiStrengthWnd CreateInstance()
		{
			return (UI_AoyiStrengthWnd)UIPackage.CreateObject("UI_AoYi","AoyiStrengthWnd");
		}

		public UI_AoyiStrengthWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_Icon = (UI_AoyiCommonItem)this.GetChildAt(2);
			m_txtStoneName = (GTextField)this.GetChildAt(3);
			m_dicList = (UI_ayIocnList)this.GetChildAt(5);
			m_objFull = (GTextField)this.GetChildAt(8);
			m_txtLevel = (GTextField)this.GetChildAt(10);
			m_progressBar = (GProgressBar)this.GetChildAt(11);
			m_txtProgressNum = (GTextField)this.GetChildAt(12);
			m_propertyList = (GList)this.GetChildAt(13);
			m_txtLevelUpDes = (GTextField)this.GetChildAt(14);
			m_btnOneKeyStrength = (GButton)this.GetChildAt(15);
			m_btnLevelUp = (GButton)this.GetChildAt(16);
			m_txtCosumeNum = (GTextField)this.GetChildAt(18);
			m_groupLevelUp = (GGroup)this.GetChildAt(19);
			m_minLevelGroup = (GGroup)this.GetChildAt(20);
			m_curLevelIcon = (UI_AoyiCommonItem)this.GetChildAt(21);
			m_nextLevelIcon = (UI_AoyiCommonItem)this.GetChildAt(22);
			m_txtCurLevelName = (GTextField)this.GetChildAt(24);
			m_txtNextLevelName = (GTextField)this.GetChildAt(25);
			m_ = (GTextField)this.GetChildAt(26);
			m__2 = (GTextField)this.GetChildAt(27);
			m_txtCoinNum = (GTextField)this.GetChildAt(29);
			m_btnBreak = (GButton)this.GetChildAt(30);
			m_breakGroup = (GGroup)this.GetChildAt(31);
		}
	}
}