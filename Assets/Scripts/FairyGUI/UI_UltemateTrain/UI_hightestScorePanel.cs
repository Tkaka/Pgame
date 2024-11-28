/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_hightestScorePanel : GComponent
	{
		public GList m_totalIncomeList;
		public GTextField m_scoreLabel;
		public GList m_scoreRewardList;
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;
		public GGraph m_scoreToucher;
		public GTextField m_scoreFormuleLabel;
		public GGroup m_scoreFormulaGroup;

		public const string URL = "ui://1wdkrtiusi7j1s";

		public static UI_hightestScorePanel CreateInstance()
		{
			return (UI_hightestScorePanel)UIPackage.CreateObject("UI_UltemateTrain","hightestScorePanel");
		}

		public UI_hightestScorePanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_totalIncomeList = (GList)this.GetChildAt(1);
			m_scoreLabel = (GTextField)this.GetChildAt(4);
			m_scoreRewardList = (GList)this.GetChildAt(5);
			m_switchLeftBtn = (GButton)this.GetChildAt(6);
			m_switchRightBtn = (GButton)this.GetChildAt(7);
			m_scoreToucher = (GGraph)this.GetChildAt(8);
			m_scoreFormuleLabel = (GTextField)this.GetChildAt(10);
			m_scoreFormulaGroup = (GGroup)this.GetChildAt(12);
		}
	}
}