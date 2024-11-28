/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BattleEnd
{
	public partial class UI_BattleEndWindow : GComponent
	{
		public Controller m_Ani;
		public GButton m_exitBtn;
		public GGraph m_winEft;
		public Transition m_Ani_2;

		public const string URL = "ui://x8kuvx95sbfs0";

		public static UI_BattleEndWindow CreateInstance()
		{
			return (UI_BattleEndWindow)UIPackage.CreateObject("UI_BattleEnd","BattleEndWindow");
		}

		public UI_BattleEndWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Ani = this.GetControllerAt(0);
			m_exitBtn = (GButton)this.GetChildAt(3);
			m_winEft = (GGraph)this.GetChildAt(6);
			m_Ani_2 = this.GetTransitionAt(0);
		}
	}
}