/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_GetedListItem : GComponent
	{
		public UI_BagListItem m_icon;
		public GTextField m_itemName;

		public const string URL = "ui://g5pgln3nl8471v";

		public static UI_GetedListItem CreateInstance()
		{
			return (UI_GetedListItem)UIPackage.CreateObject("UI_Beibao","GetedListItem");
		}

		public UI_GetedListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_icon = (UI_BagListItem)this.GetChildAt(0);
			m_itemName = (GTextField)this.GetChildAt(1);
		}
	}
}