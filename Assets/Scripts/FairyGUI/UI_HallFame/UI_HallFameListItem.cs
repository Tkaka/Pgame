/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HallFameListItem : GComponent
	{
		public GList m_TeamList;

		public const string URL = "ui://yo5kunkiilulf";

		public static UI_HallFameListItem CreateInstance()
		{
			return (UI_HallFameListItem)UIPackage.CreateObject("UI_HallFame","HallFameListItem");
		}

		public UI_HallFameListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TeamList = (GList)this.GetChildAt(0);
		}
	}
}