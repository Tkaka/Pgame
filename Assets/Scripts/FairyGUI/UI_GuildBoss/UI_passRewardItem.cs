/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_passRewardItem : GComponent
	{
		public GTextField m_bossNameLabel;
		public GLoader m_bossIconLoader;
		public GList m_rewardList;

		public const string URL = "ui://u2d86ulcjg9f6";

		public static UI_passRewardItem CreateInstance()
		{
			return (UI_passRewardItem)UIPackage.CreateObject("UI_GuildBoss","passRewardItem");
		}

		public UI_passRewardItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bossNameLabel = (GTextField)this.GetChildAt(2);
			m_bossIconLoader = (GLoader)this.GetChildAt(4);
			m_rewardList = (GList)this.GetChildAt(7);
		}
	}
}