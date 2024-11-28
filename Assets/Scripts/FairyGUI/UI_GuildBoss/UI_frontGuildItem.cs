/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_frontGuildItem : GComponent
	{
		public GTextField m_guildName;
		public GProgressBar m_bloodProgress;
		public GTextField m_perfectTip;
		public GTextField m_progressValue;

		public const string URL = "ui://u2d86ulcjg9ft";

		public static UI_frontGuildItem CreateInstance()
		{
			return (UI_frontGuildItem)UIPackage.CreateObject("UI_GuildBoss","frontGuildItem");
		}

		public UI_frontGuildItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_guildName = (GTextField)this.GetChildAt(0);
			m_bloodProgress = (GProgressBar)this.GetChildAt(1);
			m_perfectTip = (GTextField)this.GetChildAt(2);
			m_progressValue = (GTextField)this.GetChildAt(3);
		}
	}
}