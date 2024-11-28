/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleDialogWindow : GComponent
	{
		public GImage m_rightPanel;
		public GImage m_leftPanel;
		public GTextField m_dialogTxt;
		public GTextField m_leftName;
		public GTextField m_rightName;

		public const string URL = "ui://028ppdzhjkpz6y";

		public static UI_BattleDialogWindow CreateInstance()
		{
			return (UI_BattleDialogWindow)UIPackage.CreateObject("UI_Battle","BattleDialogWindow");
		}

		public UI_BattleDialogWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rightPanel = (GImage)this.GetChildAt(0);
			m_leftPanel = (GImage)this.GetChildAt(1);
			m_dialogTxt = (GTextField)this.GetChildAt(2);
			m_leftName = (GTextField)this.GetChildAt(3);
			m_rightName = (GTextField)this.GetChildAt(4);
		}
	}
}