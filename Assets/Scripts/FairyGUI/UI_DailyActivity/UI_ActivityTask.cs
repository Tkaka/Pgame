/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_ActivityTask : GComponent
	{
		public GList m_list;
		public GTextField m_title;
		public GTextField m_desc;
		public GTextField m_leftTime;

		public const string URL = "ui://0n5r1ymrjqqoa";

		public static UI_ActivityTask CreateInstance()
		{
			return (UI_ActivityTask)UIPackage.CreateObject("UI_DailyActivity","ActivityTask");
		}

		public UI_ActivityTask()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_list = (GList)this.GetChildAt(0);
			m_title = (GTextField)this.GetChildAt(1);
			m_desc = (GTextField)this.GetChildAt(2);
			m_leftTime = (GTextField)this.GetChildAt(3);
		}
	}
}