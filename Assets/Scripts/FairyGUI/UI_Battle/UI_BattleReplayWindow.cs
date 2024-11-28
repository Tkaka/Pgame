/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleReplayWindow : GComponent
	{
		public UI_BattleSet m_battleSet;
		public UI_BattleInfo m_topInfo;
		public UI_battlePetGroup m_petInfo;

		public const string URL = "ui://028ppdzhcs08g9";

		public static UI_BattleReplayWindow CreateInstance()
		{
			return (UI_BattleReplayWindow)UIPackage.CreateObject("UI_Battle","BattleReplayWindow");
		}

		public UI_BattleReplayWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_battleSet = (UI_BattleSet)this.GetChildAt(0);
			m_topInfo = (UI_BattleInfo)this.GetChildAt(1);
			m_petInfo = (UI_battlePetGroup)this.GetChildAt(2);
		}
	}
}