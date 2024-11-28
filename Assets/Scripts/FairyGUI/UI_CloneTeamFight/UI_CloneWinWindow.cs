/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_CloneWinWindow : GComponent
	{
		public GButton m_backBtn;
		public GList m_boxList;
		public GList m_rewardItemList;
		public GGroup m_rewardGroup;

		public const string URL = "ui://y12h0jfmlyhnp";

		public static UI_CloneWinWindow CreateInstance()
		{
			return (UI_CloneWinWindow)UIPackage.CreateObject("UI_CloneTeamFight","CloneWinWindow");
		}

		public UI_CloneWinWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_backBtn = (GButton)this.GetChildAt(0);
			m_boxList = (GList)this.GetChildAt(3);
			m_rewardItemList = (GList)this.GetChildAt(4);
			m_rewardGroup = (GGroup)this.GetChildAt(6);
		}
	}
}