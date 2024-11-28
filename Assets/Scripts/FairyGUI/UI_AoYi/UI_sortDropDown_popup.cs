/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_sortDropDown_popup : GComponent
	{
		public GList m_list;

		public const string URL = "ui://vexa0xryh9lhn";

		public static UI_sortDropDown_popup CreateInstance()
		{
			return (UI_sortDropDown_popup)UIPackage.CreateObject("UI_AoYi","sortDropDown_popup");
		}

		public UI_sortDropDown_popup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_list = (GList)this.GetChildAt(1);
		}
	}
}