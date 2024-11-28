/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossDamageRankWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_damageRankList;
		public GButton m_closeBtn;

		public const string URL = "ui://u2d86ulcidba17";

		public static UI_GuildBossDamageRankWindow CreateInstance()
		{
			return (UI_GuildBossDamageRankWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossDamageRankWindow");
		}

		public UI_GuildBossDamageRankWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_damageRankList = (GList)this.GetChildAt(5);
			m_closeBtn = (GButton)this.GetChildAt(6);
		}
	}
}