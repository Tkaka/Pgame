/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_guildBossItem : GComponent
	{
		public GTextField m_bubbleText;
		public GGroup m_bubbleGroup;
		public GLoader m_iconLoader;
		public GTextField m_perfictPass;
		public GButton m_bossRankBtn;
		public GTextField m_bossNameLabel;
		public GTextField m_numLabel;
		public GTextField m_additionLabel;
		public GProgressBar m_bloodProgress;
		public GTextField m_bloodProgressTip;
		public GGroup m_openGroup;
		public GGraph m_toucher;
		public GImage m_unOpenIcon;

		public const string URL = "ui://u2d86ulcjmib10";

		public static UI_guildBossItem CreateInstance()
		{
			return (UI_guildBossItem)UIPackage.CreateObject("UI_GuildBoss","guildBossItem");
		}

		public UI_guildBossItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bubbleText = (GTextField)this.GetChildAt(1);
			m_bubbleGroup = (GGroup)this.GetChildAt(2);
			m_iconLoader = (GLoader)this.GetChildAt(4);
			m_perfictPass = (GTextField)this.GetChildAt(5);
			m_bossRankBtn = (GButton)this.GetChildAt(7);
			m_bossNameLabel = (GTextField)this.GetChildAt(8);
			m_numLabel = (GTextField)this.GetChildAt(10);
			m_additionLabel = (GTextField)this.GetChildAt(11);
			m_bloodProgress = (GProgressBar)this.GetChildAt(12);
			m_bloodProgressTip = (GTextField)this.GetChildAt(13);
			m_openGroup = (GGroup)this.GetChildAt(14);
			m_toucher = (GGraph)this.GetChildAt(15);
			m_unOpenIcon = (GImage)this.GetChildAt(16);
		}
	}
}