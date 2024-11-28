/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HallFameListWindow : GComponent
	{
		public GImage m_BeiJing;
		public GList m_TimeList;
		public GTextField m_ZongXianShouZhi;
		public GButton m_CloseBtn;

		public const string URL = "ui://yo5kunkiux5q0";

		public static UI_HallFameListWindow CreateInstance()
		{
			return (UI_HallFameListWindow)UIPackage.CreateObject("UI_HallFame","HallFameListWindow");
		}

		public UI_HallFameListWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GImage)this.GetChildAt(0);
			m_TimeList = (GList)this.GetChildAt(1);
			m_ZongXianShouZhi = (GTextField)this.GetChildAt(5);
			m_CloseBtn = (GButton)this.GetChildAt(8);
		}
	}
}