/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_GZ_ListItem : GComponent
	{
		public GImage m_beijing;
		public GTextField m_describe;

		public const string URL = "ui://yjvxfmwoidnd1a";

		public static UI_SH_GZ_ListItem CreateInstance()
		{
			return (UI_SH_GZ_ListItem)UIPackage.CreateObject("UI_StriveHegemong","SH_GZ_ListItem");
		}

		public UI_SH_GZ_ListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_beijing = (GImage)this.GetChildAt(0);
			m_describe = (GTextField)this.GetChildAt(1);
		}
	}
}