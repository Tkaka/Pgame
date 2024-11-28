/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossMianWindow : GComponent
	{
		public GList m_guildBossBgList;
		public GButton m_backBtn;
		public GList m_guildBossList;
		public GGraph m_distributeBtn;
		public GGraph m_ruleBtn;
		public GGraph m_memberProgressBtn;
		public GGraph m_rewardBtn;
		public GGraph m_keyReceiveBtn;
		public GGroup m_keyReceiveGroup;
		public GProgressBar m_opponentBloodProgress;
		public GProgressBar m_selfBloodProgress;
		public GLoader m_bossIconLoader;
		public GTextField m_fightBossName;
		public GTextField m_selfGuildName;
		public GTextField m_opponentGuildName;
		public GTextField m_selfBBProgressText;
		public GTextField m_opponentBBProgressText;
		public GTextField m_noneOpponentTip;
		public GComponent m_buZhenColumn;
		public GButton m_switchRightBtn;
		public GButton m_switchLeftBtn;
		public Transition m_keyReceiveAnim;
		public Transition m_switchRightBtnAnim;
		public Transition m_switchLeftBtnAnim;

		public const string URL = "ui://u2d86ulcjg9f0";

		public static UI_GuildBossMianWindow CreateInstance()
		{
			return (UI_GuildBossMianWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossMianWindow");
		}

		public UI_GuildBossMianWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_guildBossBgList = (GList)this.GetChildAt(0);
			m_backBtn = (GButton)this.GetChildAt(2);
			m_guildBossList = (GList)this.GetChildAt(3);
			m_distributeBtn = (GGraph)this.GetChildAt(6);
			m_ruleBtn = (GGraph)this.GetChildAt(7);
			m_memberProgressBtn = (GGraph)this.GetChildAt(8);
			m_rewardBtn = (GGraph)this.GetChildAt(9);
			m_keyReceiveBtn = (GGraph)this.GetChildAt(11);
			m_keyReceiveGroup = (GGroup)this.GetChildAt(12);
			m_opponentBloodProgress = (GProgressBar)this.GetChildAt(15);
			m_selfBloodProgress = (GProgressBar)this.GetChildAt(16);
			m_bossIconLoader = (GLoader)this.GetChildAt(17);
			m_fightBossName = (GTextField)this.GetChildAt(19);
			m_selfGuildName = (GTextField)this.GetChildAt(20);
			m_opponentGuildName = (GTextField)this.GetChildAt(23);
			m_selfBBProgressText = (GTextField)this.GetChildAt(24);
			m_opponentBBProgressText = (GTextField)this.GetChildAt(25);
			m_noneOpponentTip = (GTextField)this.GetChildAt(26);
			m_buZhenColumn = (GComponent)this.GetChildAt(28);
			m_switchRightBtn = (GButton)this.GetChildAt(29);
			m_switchLeftBtn = (GButton)this.GetChildAt(30);
			m_keyReceiveAnim = this.GetTransitionAt(0);
			m_switchRightBtnAnim = this.GetTransitionAt(1);
			m_switchLeftBtnAnim = this.GetTransitionAt(2);
		}
	}
}