/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleSet : GComponent
	{
		public GButton m_pauseBtn;
		public UI_AutoBattleBtn m_autoBtn;
		public GButton m_speedBtn;

		public const string URL = "ui://028ppdzhcs08ga";

		public static UI_BattleSet CreateInstance()
		{
			return (UI_BattleSet)UIPackage.CreateObject("UI_Battle","BattleSet");
		}

		public UI_BattleSet()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_pauseBtn = (GButton)this.GetChildAt(0);
			m_autoBtn = (UI_AutoBattleBtn)this.GetChildAt(1);
			m_speedBtn = (GButton)this.GetChildAt(2);
		}
	}
}