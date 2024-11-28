/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_FetterListItem : GComponent
	{
		public GTextField m_Name;

		public const string URL = "ui://z0csav43nekc24";

		public static UI_FetterListItem CreateInstance()
		{
			return (UI_FetterListItem)UIPackage.CreateObject("UI_BuZhen","FetterListItem");
		}

		public UI_FetterListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Name = (GTextField)this.GetChildAt(0);
		}
	}
}