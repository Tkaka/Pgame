/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_buZhenPropertyItem : GComponent
	{
		public GTextField m_context;

		public const string URL = "ui://z0csav43l68bf3d";

		public static UI_buZhenPropertyItem CreateInstance()
		{
			return (UI_buZhenPropertyItem)UIPackage.CreateObject("UI_BuZhen","buZhenPropertyItem");
		}

		public UI_buZhenPropertyItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_context = (GTextField)this.GetChildAt(0);
		}
	}
}