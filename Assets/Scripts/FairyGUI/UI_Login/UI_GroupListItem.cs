/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Login
{
	public partial class UI_GroupListItem : GComponent
	{
		public GTextField m_groupName;

		public const string URL = "ui://hg19ijpaqazf10";

		public static UI_GroupListItem CreateInstance()
		{
			return (UI_GroupListItem)UIPackage.CreateObject("UI_Login","GroupListItem");
		}

		public UI_GroupListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_groupName = (GTextField)this.GetChildAt(1);
		}
	}
}