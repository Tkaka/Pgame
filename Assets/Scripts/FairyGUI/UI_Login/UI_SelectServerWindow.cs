/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Login
{
	public partial class UI_SelectServerWindow : GComponent
	{
		public GButton m_closeBtn;
		public GList m_serverList;
		public GList m_groupList;

		public const string URL = "ui://hg19ijpav5g0r";

		public static UI_SelectServerWindow CreateInstance()
		{
			return (UI_SelectServerWindow)UIPackage.CreateObject("UI_Login","SelectServerWindow");
		}

		public UI_SelectServerWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GButton)this.GetChildAt(6);
			m_serverList = (GList)this.GetChildAt(9);
			m_groupList = (GList)this.GetChildAt(10);
		}
	}
}