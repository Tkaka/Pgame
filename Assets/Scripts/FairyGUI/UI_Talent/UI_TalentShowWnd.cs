/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Talent
{
	public partial class UI_TalentShowWnd : GComponent
	{
		public GGraph m_imgBg;
		public GButton m_btnClose;
		public GLoader m_imgIcon;
		public GTextField m_txtTalentName;
		public GTextField m_txtTalentLevel;
		public GTextField m_txtDes;
		public GTextField m_txtUnLockDes;
		public GGroup m_openConditionGroup;
		public GTextField m_txtCoinNum;
		public GTextField m_txtTalentNum;
		public GComponent m_btnLevelUp;
		public GGroup m_levelUpGroup;
		public GGroup m_maxLevelGroup;

		public const string URL = "ui://erk5lfvwjqias";

		public static UI_TalentShowWnd CreateInstance()
		{
			return (UI_TalentShowWnd)UIPackage.CreateObject("UI_Talent","TalentShowWnd");
		}

		public UI_TalentShowWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GGraph)this.GetChildAt(0);
			m_btnClose = (GButton)this.GetChildAt(3);
			m_imgIcon = (GLoader)this.GetChildAt(4);
			m_txtTalentName = (GTextField)this.GetChildAt(5);
			m_txtTalentLevel = (GTextField)this.GetChildAt(6);
			m_txtDes = (GTextField)this.GetChildAt(8);
			m_txtUnLockDes = (GTextField)this.GetChildAt(10);
			m_openConditionGroup = (GGroup)this.GetChildAt(11);
			m_txtCoinNum = (GTextField)this.GetChildAt(14);
			m_txtTalentNum = (GTextField)this.GetChildAt(16);
			m_btnLevelUp = (GComponent)this.GetChildAt(17);
			m_levelUpGroup = (GGroup)this.GetChildAt(18);
			m_maxLevelGroup = (GGroup)this.GetChildAt(20);
		}
	}
}