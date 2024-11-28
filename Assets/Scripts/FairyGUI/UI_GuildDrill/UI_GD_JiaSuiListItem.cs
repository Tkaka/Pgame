/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_JiaSuiListItem : GComponent
	{
		public GLoader m_touxiang;
		public GTextField m_name;
		public GTextField m_level;
		public GImage m_HaoYouBiaoJi;
		public GButton m_jiasuBtn;

		public const string URL = "ui://wwlsouxzkzeu7";

		public static UI_GD_JiaSuiListItem CreateInstance()
		{
			return (UI_GD_JiaSuiListItem)UIPackage.CreateObject("UI_GuildDrill","GD_JiaSuiListItem");
		}

		public UI_GD_JiaSuiListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_touxiang = (GLoader)this.GetChildAt(2);
			m_name = (GTextField)this.GetChildAt(3);
			m_level = (GTextField)this.GetChildAt(5);
			m_HaoYouBiaoJi = (GImage)this.GetChildAt(6);
			m_jiasuBtn = (GButton)this.GetChildAt(7);
		}
	}
}