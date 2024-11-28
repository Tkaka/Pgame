/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_WndDailyRoot : GComponent
	{
		public UI_ActivityTabList m_table;
		public GButton m_contentBg;
		public GComponent m_top;

		public const string URL = "ui://0n5r1ymrjqqo0";

		public static UI_WndDailyRoot CreateInstance()
		{
			return (UI_WndDailyRoot)UIPackage.CreateObject("UI_DailyActivity","WndDailyRoot");
		}

		public UI_WndDailyRoot()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_table = (UI_ActivityTabList)this.GetChildAt(1);
			m_contentBg = (GButton)this.GetChildAt(2);
			m_top = (GComponent)this.GetChildAt(3);
		}
	}
}