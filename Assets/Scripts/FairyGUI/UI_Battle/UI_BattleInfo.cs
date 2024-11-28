/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleInfo : GComponent
	{
		public GTextField m_countDown;
		public GImage m_wuQiongImg;
		public GTextField m_turnTxt;
		public GGroup m_countDownG;
		public UI_PlayerInfoPanel m_playerInfo;
		public UI_EnemyInfoPanel m_enemyInfo;
		public GTextField m_waveTxt;
		public GTextField m_goldNum;

		public const string URL = "ui://028ppdzhlkuwsh2";

		public static UI_BattleInfo CreateInstance()
		{
			return (UI_BattleInfo)UIPackage.CreateObject("UI_Battle","BattleInfo");
		}

		public UI_BattleInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_countDown = (GTextField)this.GetChildAt(1);
			m_wuQiongImg = (GImage)this.GetChildAt(2);
			m_turnTxt = (GTextField)this.GetChildAt(3);
			m_countDownG = (GGroup)this.GetChildAt(4);
			m_playerInfo = (UI_PlayerInfoPanel)this.GetChildAt(5);
			m_enemyInfo = (UI_EnemyInfoPanel)this.GetChildAt(6);
			m_waveTxt = (GTextField)this.GetChildAt(8);
			m_goldNum = (GTextField)this.GetChildAt(11);
		}
	}
}