/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_guildBossRewardItem : GComponent
	{
		public GTextField m_rankLabel;
		public GList m_rewardList;

		public const string URL = "ui://u2d86ulcjg9fl";

		public static UI_guildBossRewardItem CreateInstance()
		{
			return (UI_guildBossRewardItem)UIPackage.CreateObject("UI_GuildBoss","guildBossRewardItem");
		}

		public UI_guildBossRewardItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rankLabel = (GTextField)this.GetChildAt(0);
			m_rewardList = (GList)this.GetChildAt(1);
		}
	}
}