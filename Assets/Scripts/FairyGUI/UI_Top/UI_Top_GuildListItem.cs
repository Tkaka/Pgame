/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Top
{
	public partial class UI_Top_GuildListItem : GComponent
	{
		public GLoader m_paiming_Icon;
		public GTextField m_paiming_number;
		public GLoader m_huizhang;
		public GLoader m_type_icon;
		public GTextField m_type_name;
		public GTextField m_guildName;
		public GTextField m_guildLevel;
		public GTextField m_shezhang;
		public GTextField m_zongzhanli;
		public GGroup m_SheTuan;
		public GTextField m_weijiaru;
		public GTextField m_ziji;

		public const string URL = "ui://y4tkaqbbrxbld";

		public static UI_Top_GuildListItem CreateInstance()
		{
			return (UI_Top_GuildListItem)UIPackage.CreateObject("UI_Top","Top_GuildListItem");
		}

		public UI_Top_GuildListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_paiming_Icon = (GLoader)this.GetChildAt(1);
			m_paiming_number = (GTextField)this.GetChildAt(2);
			m_huizhang = (GLoader)this.GetChildAt(3);
			m_type_icon = (GLoader)this.GetChildAt(4);
			m_type_name = (GTextField)this.GetChildAt(5);
			m_guildName = (GTextField)this.GetChildAt(6);
			m_guildLevel = (GTextField)this.GetChildAt(8);
			m_shezhang = (GTextField)this.GetChildAt(9);
			m_zongzhanli = (GTextField)this.GetChildAt(10);
			m_SheTuan = (GGroup)this.GetChildAt(11);
			m_weijiaru = (GTextField)this.GetChildAt(12);
			m_ziji = (GTextField)this.GetChildAt(13);
		}
	}
}