/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_guildBossRankTip : GComponent
	{
		public GTextField m_tip;

		public const string URL = "ui://u2d86ulcjg9fk";

		public static UI_guildBossRankTip CreateInstance()
		{
			return (UI_guildBossRankTip)UIPackage.CreateObject("UI_GuildBoss","guildBossRankTip");
		}

		public UI_guildBossRankTip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tip = (GTextField)this.GetChildAt(1);
		}
	}
}