/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_GuildBattleMianWindow : GComponent
	{
		public GComponent m_commonTop;
		public GButton m_zhenRongBtn;
		public GButton m_zuoRiZhanKuanBtn;
		public GButton m_ruleBtn;
		public GButton m_exchangeBtn;
		public GButton m_rankBtn;
		public GTextField m_totalScoreLabel;
		public GButton m_showNumDetailBtn;
		public GTextField m_maxLianShenName;
		public GTextField m_lianShenNumLabel;
		public GGroup m_UnBaoMingTextGroup;
		public GTextField m_curRankLabel;
		public GTextField m_curNumLabel;
		public GGroup m_baoMingTextGroup;
		public GButton m_baoMingBtn;
		public GTextField m_baoMingTip;
		public GTextField m_guildBattleTypeLabel;
		public GTextField m_applyLimitTip;
		public GButton m_applyNoticeBtn;
		public GGroup m_normalBaoMingGroup;
		public GList m_zhanKuanList;
		public GTextField m_guildBattleType;
		public GTextField m_chanCiLabel;
		public GTextField m_battleLimitType;
		public GGroup m_normalBattleGroup;
		public GGroup m_noramlBattleGroup;
		public GLoader m_jueSaiLeftGuildIcon;
		public GTextField m_jueSaiLeftGuildName;
		public GTextField m_jueSaiLeftguildApplyNum;
		public GLoader m_jueSaiRightGuildIcon;
		public GTextField m_jueSaiRightGuildName;
		public GTextField m_jueSaiRightguildApplyNum;
		public GButton m_enterZhanChangBtn;
		public GList m_jueSaiZhanKuanList;
		public GGroup m_jueSaiBattleGroup;
		public UI_gbGuildItem m_leftGuild;
		public UI_gbGuildItem m_rightGuild;
		public GButton m_jueSaibaoMingBtn;
		public GTextField m_jueSaibaoMingTip;
		public GTextField m_applyNum;
		public GButton m_watchApplyDetailBtn;
		public GGroup m_jueSaiBaoMingGroup;
		public GGroup m_jueSaiBattleGroup_2;

		public const string URL = "ui://xj95784r11d420";

		public static UI_GuildBattleMianWindow CreateInstance()
		{
			return (UI_GuildBattleMianWindow)UIPackage.CreateObject("UI_GuildBattle","GuildBattleMianWindow");
		}

		public UI_GuildBattleMianWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_commonTop = (GComponent)this.GetChildAt(3);
			m_zhenRongBtn = (GButton)this.GetChildAt(7);
			m_zuoRiZhanKuanBtn = (GButton)this.GetChildAt(8);
			m_ruleBtn = (GButton)this.GetChildAt(9);
			m_exchangeBtn = (GButton)this.GetChildAt(10);
			m_rankBtn = (GButton)this.GetChildAt(11);
			m_totalScoreLabel = (GTextField)this.GetChildAt(18);
			m_showNumDetailBtn = (GButton)this.GetChildAt(19);
			m_maxLianShenName = (GTextField)this.GetChildAt(21);
			m_lianShenNumLabel = (GTextField)this.GetChildAt(23);
			m_UnBaoMingTextGroup = (GGroup)this.GetChildAt(24);
			m_curRankLabel = (GTextField)this.GetChildAt(27);
			m_curNumLabel = (GTextField)this.GetChildAt(28);
			m_baoMingTextGroup = (GGroup)this.GetChildAt(29);
			m_baoMingBtn = (GButton)this.GetChildAt(30);
			m_baoMingTip = (GTextField)this.GetChildAt(31);
			m_guildBattleTypeLabel = (GTextField)this.GetChildAt(32);
			m_applyLimitTip = (GTextField)this.GetChildAt(33);
			m_applyNoticeBtn = (GButton)this.GetChildAt(34);
			m_normalBaoMingGroup = (GGroup)this.GetChildAt(35);
			m_zhanKuanList = (GList)this.GetChildAt(36);
			m_guildBattleType = (GTextField)this.GetChildAt(37);
			m_chanCiLabel = (GTextField)this.GetChildAt(39);
			m_battleLimitType = (GTextField)this.GetChildAt(40);
			m_normalBattleGroup = (GGroup)this.GetChildAt(41);
			m_noramlBattleGroup = (GGroup)this.GetChildAt(42);
			m_jueSaiLeftGuildIcon = (GLoader)this.GetChildAt(44);
			m_jueSaiLeftGuildName = (GTextField)this.GetChildAt(45);
			m_jueSaiLeftguildApplyNum = (GTextField)this.GetChildAt(46);
			m_jueSaiRightGuildIcon = (GLoader)this.GetChildAt(47);
			m_jueSaiRightGuildName = (GTextField)this.GetChildAt(48);
			m_jueSaiRightguildApplyNum = (GTextField)this.GetChildAt(49);
			m_enterZhanChangBtn = (GButton)this.GetChildAt(50);
			m_jueSaiZhanKuanList = (GList)this.GetChildAt(51);
			m_jueSaiBattleGroup = (GGroup)this.GetChildAt(52);
			m_leftGuild = (UI_gbGuildItem)this.GetChildAt(55);
			m_rightGuild = (UI_gbGuildItem)this.GetChildAt(56);
			m_jueSaibaoMingBtn = (GButton)this.GetChildAt(57);
			m_jueSaibaoMingTip = (GTextField)this.GetChildAt(58);
			m_applyNum = (GTextField)this.GetChildAt(60);
			m_watchApplyDetailBtn = (GButton)this.GetChildAt(61);
			m_jueSaiBaoMingGroup = (GGroup)this.GetChildAt(62);
			m_jueSaiBattleGroup_2 = (GGroup)this.GetChildAt(63);
		}
	}
}