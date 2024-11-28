/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_rankGuildItem : GComponent
	{
		public GLoader m_rankIcon;
		public GTextField m_rankNum;
		public GTextField m_sheZhangName;
		public GTextField m_totalFightPower;
		public GLoader m_type_icon;
		public GLoader m_huiZhanLoader;
		public GTextField m_guildName;
		public GTextField m_guildLevel;
		public GGroup m_SheTuan;
		public GTextField m_unJoinTip;
		public GTextField m_selfIcon;
		public GGraph m_toucher;

		public const string URL = "ui://42sthz3thf5jxr6";

		public static UI_rankGuildItem CreateInstance()
		{
			return (UI_rankGuildItem)UIPackage.CreateObject("UI_Common","rankGuildItem");
		}

		public UI_rankGuildItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rankIcon = (GLoader)this.GetChildAt(1);
			m_rankNum = (GTextField)this.GetChildAt(2);
			m_sheZhangName = (GTextField)this.GetChildAt(3);
			m_totalFightPower = (GTextField)this.GetChildAt(4);
			m_type_icon = (GLoader)this.GetChildAt(5);
			m_huiZhanLoader = (GLoader)this.GetChildAt(6);
			m_guildName = (GTextField)this.GetChildAt(7);
			m_guildLevel = (GTextField)this.GetChildAt(8);
			m_SheTuan = (GGroup)this.GetChildAt(9);
			m_unJoinTip = (GTextField)this.GetChildAt(10);
			m_selfIcon = (GTextField)this.GetChildAt(11);
			m_toucher = (GGraph)this.GetChildAt(12);
		}
	}
}