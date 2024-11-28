/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossLevelWindow : GComponent
	{
		public GButton m_backBtn;
		public GTextField m_specialDesLabel;
		public GList m_firstRewardList;
		public GTextField m_timesLabel;
		public GButton m_startFightBtn;
		public GButton m_receivedBtn;
		public GButton m_alreadyReceivedBtn;
		public GProgressBar m_bossBloodProgress;
		public GComponent m_specialDamgeItem1;
		public GComponent m_specialDamgeItem2;
		public GComponent m_specialDamgeItem3;
		public GComponent m_specialDamgeItem4;
		public GComponent m_specialDamgeItem5;
		public GList m_frontGuildList;
		public GTextField m_bubbleLabel;
		public GGroup m_bubleGroup;
		public GGraph m_modelPos;
		public GTextField m_nameLabel;
		public GButton m_damageRnakBtn;
		public GProgressBar m_bloodRemainProgress;
		public GTextField m_bloodReaminLabel;
		public GGroup m_bossRemainBloodGroup;
		public GGroup m_passGroup;

		public const string URL = "ui://u2d86ulcjg9fo";

		public static UI_GuildBossLevelWindow CreateInstance()
		{
			return (UI_GuildBossLevelWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossLevelWindow");
		}

		public UI_GuildBossLevelWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_backBtn = (GButton)this.GetChildAt(2);
			m_specialDesLabel = (GTextField)this.GetChildAt(6);
			m_firstRewardList = (GList)this.GetChildAt(8);
			m_timesLabel = (GTextField)this.GetChildAt(10);
			m_startFightBtn = (GButton)this.GetChildAt(11);
			m_receivedBtn = (GButton)this.GetChildAt(12);
			m_alreadyReceivedBtn = (GButton)this.GetChildAt(13);
			m_bossBloodProgress = (GProgressBar)this.GetChildAt(18);
			m_specialDamgeItem1 = (GComponent)this.GetChildAt(25);
			m_specialDamgeItem2 = (GComponent)this.GetChildAt(26);
			m_specialDamgeItem3 = (GComponent)this.GetChildAt(27);
			m_specialDamgeItem4 = (GComponent)this.GetChildAt(28);
			m_specialDamgeItem5 = (GComponent)this.GetChildAt(29);
			m_frontGuildList = (GList)this.GetChildAt(35);
			m_bubbleLabel = (GTextField)this.GetChildAt(37);
			m_bubleGroup = (GGroup)this.GetChildAt(38);
			m_modelPos = (GGraph)this.GetChildAt(39);
			m_nameLabel = (GTextField)this.GetChildAt(40);
			m_damageRnakBtn = (GButton)this.GetChildAt(41);
			m_bloodRemainProgress = (GProgressBar)this.GetChildAt(42);
			m_bloodReaminLabel = (GTextField)this.GetChildAt(43);
			m_bossRemainBloodGroup = (GGroup)this.GetChildAt(44);
			m_passGroup = (GGroup)this.GetChildAt(47);
		}
	}
}