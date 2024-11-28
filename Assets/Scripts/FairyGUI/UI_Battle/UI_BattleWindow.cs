/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleWindow : GComponent
	{
		public UI_ComboTip_6 m_combo6;
		public UI_ComboTip m_comboCommon;
		public UI_EvaluateTip m_evaluateTip;
		public GTextField m_countDown;
		public GImage m_wuQiongImg;
		public GTextField m_turnTxt;
		public GGroup m_countDownG;
		public GButton m_pauseBtn;
		public UI_AutoBattleBtn m_autoBtn;
		public GButton m_speedBtn;
		public UI_PlayerInfoPanel m_playerInfo;
		public UI_EnemyInfoPanel m_enemyInfo;
		public GTextField m_waveTxt;
		public GTextField m_goldNum;
		public GGraph m_toucher;
		public GList m_petList;
		public GGroup m_petGroup;

		public const string URL = "ui://028ppdzhq2pd0";

		public static UI_BattleWindow CreateInstance()
		{
			return (UI_BattleWindow)UIPackage.CreateObject("UI_Battle","BattleWindow");
		}

		public UI_BattleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_combo6 = (UI_ComboTip_6)this.GetChildAt(0);
			m_comboCommon = (UI_ComboTip)this.GetChildAt(1);
			m_evaluateTip = (UI_EvaluateTip)this.GetChildAt(2);
			m_countDown = (GTextField)this.GetChildAt(4);
			m_wuQiongImg = (GImage)this.GetChildAt(5);
			m_turnTxt = (GTextField)this.GetChildAt(6);
			m_countDownG = (GGroup)this.GetChildAt(7);
			m_pauseBtn = (GButton)this.GetChildAt(8);
			m_autoBtn = (UI_AutoBattleBtn)this.GetChildAt(9);
			m_speedBtn = (GButton)this.GetChildAt(10);
			m_playerInfo = (UI_PlayerInfoPanel)this.GetChildAt(12);
			m_enemyInfo = (UI_EnemyInfoPanel)this.GetChildAt(13);
			m_waveTxt = (GTextField)this.GetChildAt(15);
			m_goldNum = (GTextField)this.GetChildAt(18);
			m_toucher = (GGraph)this.GetChildAt(20);
			m_petList = (GList)this.GetChildAt(22);
			m_petGroup = (GGroup)this.GetChildAt(23);
		}
	}
}