/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_TrainScoreRewardWindow : GComponent
	{
		public Controller m_rewardCtrl;
		public UI_hightestScorePanel m_hightestScorePanel;
		public UI_trainRankRewardPanel m_trainRankRewardPanel;
		public GButton m_backBtn;

		public const string URL = "ui://1wdkrtiusi7j1j";

		public static UI_TrainScoreRewardWindow CreateInstance()
		{
			return (UI_TrainScoreRewardWindow)UIPackage.CreateObject("UI_UltemateTrain","TrainScoreRewardWindow");
		}

		public UI_TrainScoreRewardWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rewardCtrl = this.GetControllerAt(0);
			m_hightestScorePanel = (UI_hightestScorePanel)this.GetChildAt(5);
			m_trainRankRewardPanel = (UI_trainRankRewardPanel)this.GetChildAt(6);
			m_backBtn = (GButton)this.GetChildAt(8);
		}
	}
}