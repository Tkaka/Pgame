/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_inviteFriendItem : GComponent
	{
		public GLoader m_boardIcon;
		public GLoader m_iconLoader;
		public GTextField m_nameLabel;
		public GTextField m_fightPowerLabel;
		public GTextField m_levelLabel;
		public GButton m_inviteBtn;

		public const string URL = "ui://y12h0jfmiho810";

		public static UI_inviteFriendItem CreateInstance()
		{
			return (UI_inviteFriendItem)UIPackage.CreateObject("UI_CloneTeamFight","inviteFriendItem");
		}

		public UI_inviteFriendItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardIcon = (GLoader)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
			m_nameLabel = (GTextField)this.GetChildAt(3);
			m_fightPowerLabel = (GTextField)this.GetChildAt(5);
			m_levelLabel = (GTextField)this.GetChildAt(6);
			m_inviteBtn = (GButton)this.GetChildAt(7);
		}
	}
}