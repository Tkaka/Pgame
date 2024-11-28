/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_ActivityTabList : GComponent
	{
		public Controller m_ctrl;
		public GList m_list;

		public const string URL = "ui://0n5r1ymrjqqo2";

		public static UI_ActivityTabList CreateInstance()
		{
			return (UI_ActivityTabList)UIPackage.CreateObject("UI_DailyActivity","ActivityTabList");
		}

		public UI_ActivityTabList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_list = (GList)this.GetChildAt(0);
		}
	}
}