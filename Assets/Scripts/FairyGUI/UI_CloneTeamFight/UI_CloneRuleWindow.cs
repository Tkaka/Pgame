/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_CloneRuleWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_contentList;
		public GButton m_closeBtn;

		public const string URL = "ui://y12h0jfmlyhno";

		public static UI_CloneRuleWindow CreateInstance()
		{
			return (UI_CloneRuleWindow)UIPackage.CreateObject("UI_CloneTeamFight","CloneRuleWindow");
		}

		public UI_CloneRuleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_contentList = (GList)this.GetChildAt(2);
			m_closeBtn = (GButton)this.GetChildAt(4);
		}
	}
}