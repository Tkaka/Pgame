/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_CloneInviteFriendWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_friendList;
		public GTextField m_pageIndexTipLabel;
		public GButton m_closeBtn;
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;

		public const string URL = "ui://y12h0jfm7o4au";

		public static UI_CloneInviteFriendWindow CreateInstance()
		{
			return (UI_CloneInviteFriendWindow)UIPackage.CreateObject("UI_CloneTeamFight","CloneInviteFriendWindow");
		}

		public UI_CloneInviteFriendWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_friendList = (GList)this.GetChildAt(4);
			m_pageIndexTipLabel = (GTextField)this.GetChildAt(5);
			m_closeBtn = (GButton)this.GetChildAt(6);
			m_switchLeftBtn = (GButton)this.GetChildAt(7);
			m_switchRightBtn = (GButton)this.GetChildAt(8);
		}
	}
}