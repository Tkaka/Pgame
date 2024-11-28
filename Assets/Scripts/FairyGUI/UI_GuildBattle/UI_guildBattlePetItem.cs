/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_guildBattlePetItem : GComponent
	{
		public GImage m_bg;
		public GButton m_petItem;

		public const string URL = "ui://xj95784rmtsg2i";

		public static UI_guildBattlePetItem CreateInstance()
		{
			return (UI_guildBattlePetItem)UIPackage.CreateObject("UI_GuildBattle","guildBattlePetItem");
		}

		public UI_guildBattlePetItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GImage)this.GetChildAt(0);
			m_petItem = (GButton)this.GetChildAt(1);
		}
	}
}