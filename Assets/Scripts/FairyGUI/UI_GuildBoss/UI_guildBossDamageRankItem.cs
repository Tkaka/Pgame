/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_guildBossDamageRankItem : GComponent
	{
		public GImage m_bg;
		public GTextField m_rank;
		public GTextField m_nameLabel;
		public GTextField m_damageLabel;

		public const string URL = "ui://u2d86ulcidba18";

		public static UI_guildBossDamageRankItem CreateInstance()
		{
			return (UI_guildBossDamageRankItem)UIPackage.CreateObject("UI_GuildBoss","guildBossDamageRankItem");
		}

		public UI_guildBossDamageRankItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GImage)this.GetChildAt(0);
			m_rank = (GTextField)this.GetChildAt(1);
			m_nameLabel = (GTextField)this.GetChildAt(2);
			m_damageLabel = (GTextField)this.GetChildAt(3);
		}
	}
}