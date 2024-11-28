/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_VIP
{
	public partial class UI_VipMainWnd : GComponent
	{
		public GComponent m_commonTop;
		public GList m_pageList;
		public GButton m_btnLeft;
		public GButton m_btnRight;
		public UI_VipTitle m_vipTitle;

		public const string URL = "ui://7pvd5vi4qaa20";

		public static UI_VipMainWnd CreateInstance()
		{
			return (UI_VipMainWnd)UIPackage.CreateObject("UI_VIP","VipMainWnd");
		}

		public UI_VipMainWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_commonTop = (GComponent)this.GetChildAt(1);
			m_pageList = (GList)this.GetChildAt(5);
			m_btnLeft = (GButton)this.GetChildAt(6);
			m_btnRight = (GButton)this.GetChildAt(7);
			m_vipTitle = (UI_VipTitle)this.GetChildAt(8);
		}
	}
}