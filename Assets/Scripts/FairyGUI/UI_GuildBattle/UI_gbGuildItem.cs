/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_gbGuildItem : GComponent
	{
		public GLoader m_guildIconLoader;
		public GTextField m_guildNameLabel;
		public GTextField m_numLabel;
		public GTextField m_resultLabel;

		public const string URL = "ui://xj95784rsbde32";

		public static UI_gbGuildItem CreateInstance()
		{
			return (UI_gbGuildItem)UIPackage.CreateObject("UI_GuildBattle","gbGuildItem");
		}

		public UI_gbGuildItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_guildIconLoader = (GLoader)this.GetChildAt(0);
			m_guildNameLabel = (GTextField)this.GetChildAt(1);
			m_numLabel = (GTextField)this.GetChildAt(2);
			m_resultLabel = (GTextField)this.GetChildAt(3);
		}
	}
}