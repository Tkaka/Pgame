/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_eliteSupperSaoDangWnd : GComponent
	{
		public GButton m_btnSaoDangSet;
		public GButton m_btnOneKeySaoDang;
		public GButton m_btnClose;
		public GList m_mainList;
		public UI_powerCoumeTip m_comsumePower;

		public const string URL = "ui://34cd5b4hjr2f1x";

		public static UI_eliteSupperSaoDangWnd CreateInstance()
		{
			return (UI_eliteSupperSaoDangWnd)UIPackage.CreateObject("UI_SaoDangJieSuan","eliteSupperSaoDangWnd");
		}

		public UI_eliteSupperSaoDangWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnSaoDangSet = (GButton)this.GetChildAt(1);
			m_btnOneKeySaoDang = (GButton)this.GetChildAt(2);
			m_btnClose = (GButton)this.GetChildAt(4);
			m_mainList = (GList)this.GetChildAt(5);
			m_comsumePower = (UI_powerCoumeTip)this.GetChildAt(6);
		}
	}
}