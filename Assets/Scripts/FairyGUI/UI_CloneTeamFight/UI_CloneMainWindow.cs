/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_CloneMainWindow : GComponent
	{
		public GButton m_backBtn;
		public GButton m_ruleBtn;
		public GTextField m_cardNum;
		public GList m_cloneTeamList;

		public const string URL = "ui://y12h0jfmlyhn0";

		public static UI_CloneMainWindow CreateInstance()
		{
			return (UI_CloneMainWindow)UIPackage.CreateObject("UI_CloneTeamFight","CloneMainWindow");
		}

		public UI_CloneMainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_backBtn = (GButton)this.GetChildAt(1);
			m_ruleBtn = (GButton)this.GetChildAt(4);
			m_cardNum = (GTextField)this.GetChildAt(8);
			m_cloneTeamList = (GList)this.GetChildAt(10);
		}
	}
}