/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_zhanKuanRoleItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GTextField m_resultLabel;
		public GTextField m_levelLabel;
		public GTextField m_nameLabel;
		public GTextField m_guildName;
		public GProgressBar m_hpProgress;
		public GTextField m_teamLabel;

		public const string URL = "ui://xj95784riouo2w";

		public static UI_zhanKuanRoleItem CreateInstance()
		{
			return (UI_zhanKuanRoleItem)UIPackage.CreateObject("UI_GuildBattle","zhanKuanRoleItem");
		}

		public UI_zhanKuanRoleItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
			m_resultLabel = (GTextField)this.GetChildAt(3);
			m_levelLabel = (GTextField)this.GetChildAt(4);
			m_nameLabel = (GTextField)this.GetChildAt(5);
			m_guildName = (GTextField)this.GetChildAt(6);
			m_hpProgress = (GProgressBar)this.GetChildAt(7);
			m_teamLabel = (GTextField)this.GetChildAt(8);
		}
	}
}