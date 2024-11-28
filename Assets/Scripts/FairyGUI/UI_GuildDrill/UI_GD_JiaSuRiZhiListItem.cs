/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_JiaSuRiZhiListItem : GComponent
	{
		public GTextField m_string;

		public const string URL = "ui://wwlsouxzkzeuf";

		public static UI_GD_JiaSuRiZhiListItem CreateInstance()
		{
			return (UI_GD_JiaSuRiZhiListItem)UIPackage.CreateObject("UI_GuildDrill","GD_JiaSuRiZhiListItem");
		}

		public UI_GD_JiaSuRiZhiListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_string = (GTextField)this.GetChildAt(1);
		}
	}
}