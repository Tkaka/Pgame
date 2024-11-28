/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Login
{
	public partial class UI_ServerListItem : GComponent
	{
		public GTextField m_serverName;

		public const string URL = "ui://hg19ijpaqazfz";

		public static UI_ServerListItem CreateInstance()
		{
			return (UI_ServerListItem)UIPackage.CreateObject("UI_Login","ServerListItem");
		}

		public UI_ServerListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_serverName = (GTextField)this.GetChildAt(2);
		}
	}
}