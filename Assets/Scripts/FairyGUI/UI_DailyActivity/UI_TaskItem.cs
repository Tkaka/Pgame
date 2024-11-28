/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_TaskItem : GComponent
	{
		public GTextField m_cost;
		public GTextField m_propress;
		public GList m_list;
		public GButton m_lqBtn;

		public const string URL = "ui://0n5r1ymrjqqo7";

		public static UI_TaskItem CreateInstance()
		{
			return (UI_TaskItem)UIPackage.CreateObject("UI_DailyActivity","TaskItem");
		}

		public UI_TaskItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_cost = (GTextField)this.GetChildAt(1);
			m_propress = (GTextField)this.GetChildAt(2);
			m_list = (GList)this.GetChildAt(3);
			m_lqBtn = (GButton)this.GetChildAt(4);
		}
	}
}