/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_IconSelectWindow : GComponent
	{
		public GButton m_btnClose;
		public GList m_iconList;

		public const string URL = "ui://3xs7lfyxgawd1d";

		public static UI_IconSelectWindow CreateInstance()
		{
			return (UI_IconSelectWindow)UIPackage.CreateObject("UI_Arena","IconSelectWindow");
		}

		public UI_IconSelectWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_iconList = (GList)this.GetChildAt(3);
		}
	}
}