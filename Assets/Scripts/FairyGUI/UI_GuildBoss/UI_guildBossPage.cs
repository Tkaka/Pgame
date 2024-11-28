/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_guildBossPage : GComponent
	{
		public UI_guildBossItem m_guildBossItem1;
		public UI_guildBossItem m_guildBossItem2;
		public UI_guildBossItem m_guildBossItem3;
		public UI_guildBossItem m_guildBossItem4;

		public const string URL = "ui://u2d86ulcjmib11";

		public static UI_guildBossPage CreateInstance()
		{
			return (UI_guildBossPage)UIPackage.CreateObject("UI_GuildBoss","guildBossPage");
		}

		public UI_guildBossPage()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_guildBossItem1 = (UI_guildBossItem)this.GetChildAt(1);
			m_guildBossItem2 = (UI_guildBossItem)this.GetChildAt(2);
			m_guildBossItem3 = (UI_guildBossItem)this.GetChildAt(3);
			m_guildBossItem4 = (UI_guildBossItem)this.GetChildAt(4);
		}
	}
}