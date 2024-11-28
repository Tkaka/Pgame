/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_CloneTeamWindow : GComponent
	{
		public GButton m_backBtn;
		public GButton m_forbidBtn;
		public GGroup m_forbidGroup;
		public GButton m_ruleBtn;
		public GGraph m_modelPos;
		public GGraph m_modelToucher;
		public GTextField m_nameLabel;
		public GTextField m_bubbleLabel;
		public GGroup m_bubbleGroup;
		public GTextField m_teamName;
		public GList m_teammateList;
		public GRichTextField m_progressGetTipLabel;
		public GButton m_sheTuanInviteBtn;
		public GButton m_worldInviteBtn;
		public GGroup m_inviteGroup;
		public GButton m_noticeTeammateBtn;
		public GButton m_fightStartBtn;
		public GButton m_leaveBtn;
		public GTextField m_finishTipLabel;
		public Transition m_t0;

		public const string URL = "ui://y12h0jfmlyhnc";

		public static UI_CloneTeamWindow CreateInstance()
		{
			return (UI_CloneTeamWindow)UIPackage.CreateObject("UI_CloneTeamFight","CloneTeamWindow");
		}

		public UI_CloneTeamWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_backBtn = (GButton)this.GetChildAt(1);
			m_forbidBtn = (GButton)this.GetChildAt(2);
			m_forbidGroup = (GGroup)this.GetChildAt(4);
			m_ruleBtn = (GButton)this.GetChildAt(7);
			m_modelPos = (GGraph)this.GetChildAt(10);
			m_modelToucher = (GGraph)this.GetChildAt(11);
			m_nameLabel = (GTextField)this.GetChildAt(12);
			m_bubbleLabel = (GTextField)this.GetChildAt(14);
			m_bubbleGroup = (GGroup)this.GetChildAt(15);
			m_teamName = (GTextField)this.GetChildAt(18);
			m_teammateList = (GList)this.GetChildAt(19);
			m_progressGetTipLabel = (GRichTextField)this.GetChildAt(21);
			m_sheTuanInviteBtn = (GButton)this.GetChildAt(22);
			m_worldInviteBtn = (GButton)this.GetChildAt(23);
			m_inviteGroup = (GGroup)this.GetChildAt(24);
			m_noticeTeammateBtn = (GButton)this.GetChildAt(25);
			m_fightStartBtn = (GButton)this.GetChildAt(26);
			m_leaveBtn = (GButton)this.GetChildAt(27);
			m_finishTipLabel = (GTextField)this.GetChildAt(28);
			m_t0 = this.GetTransitionAt(0);
		}
	}
}