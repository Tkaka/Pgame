/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleVictoryWindow : GComponent
	{
		public Transition m_t1;

		public const string URL = "ui://028ppdzhumw7ex";

		public static UI_BattleVictoryWindow CreateInstance()
		{
			return (UI_BattleVictoryWindow)UIPackage.CreateObject("UI_Battle","BattleVictoryWindow");
		}

		public UI_BattleVictoryWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_t1 = this.GetTransitionAt(0);
		}
	}
}