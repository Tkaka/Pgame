/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_DuiHuanItem : GComponent
	{
		public GTextField m_cost;
		public GTextField m_target;
		public GTextField m_progress;
		public GList m_list;
		public GButton m_targetIcon;
		public GButton m_dhBtn;

		public const string URL = "ui://0n5r1ymrjqqo4";

		public static UI_DuiHuanItem CreateInstance()
		{
			return (UI_DuiHuanItem)UIPackage.CreateObject("UI_DailyActivity","DuiHuanItem");
		}

		public UI_DuiHuanItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_cost = (GTextField)this.GetChildAt(1);
			m_target = (GTextField)this.GetChildAt(3);
			m_progress = (GTextField)this.GetChildAt(4);
			m_list = (GList)this.GetChildAt(5);
			m_targetIcon = (GButton)this.GetChildAt(7);
			m_dhBtn = (GButton)this.GetChildAt(8);
		}
	}
}