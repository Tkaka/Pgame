/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_guildBossDistributeItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GTextField m_nameLabel;
		public GTextField m_bossName;
		public GComponent m_rewardItem;
		public GTextField m_getTimeLabel;

		public const string URL = "ui://u2d86ulcjg9fn";

		public static UI_guildBossDistributeItem CreateInstance()
		{
			return (UI_guildBossDistributeItem)UIPackage.CreateObject("UI_GuildBoss","guildBossDistributeItem");
		}

		public UI_guildBossDistributeItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
			m_nameLabel = (GTextField)this.GetChildAt(3);
			m_bossName = (GTextField)this.GetChildAt(4);
			m_rewardItem = (GComponent)this.GetChildAt(5);
			m_getTimeLabel = (GTextField)this.GetChildAt(7);
		}
	}
}