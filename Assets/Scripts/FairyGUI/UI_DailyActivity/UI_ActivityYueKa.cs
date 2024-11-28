/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_ActivityYueKa : GComponent
	{
		public GTextField m_title;
		public GTextField m_desc;
		public GTextField m_leftTitle;
		public GTextField m_rightTitle;
		public GTextField m_left1;
		public GTextField m_left2;
		public GButton m_rightBtn;
		public GButton m_leftBtn;

		public const string URL = "ui://0n5r1ymrjqqob";

		public static UI_ActivityYueKa CreateInstance()
		{
			return (UI_ActivityYueKa)UIPackage.CreateObject("UI_DailyActivity","ActivityYueKa");
		}

		public UI_ActivityYueKa()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title = (GTextField)this.GetChildAt(0);
			m_desc = (GTextField)this.GetChildAt(1);
			m_leftTitle = (GTextField)this.GetChildAt(5);
			m_rightTitle = (GTextField)this.GetChildAt(6);
			m_left1 = (GTextField)this.GetChildAt(7);
			m_left2 = (GTextField)this.GetChildAt(8);
			m_rightBtn = (GButton)this.GetChildAt(9);
			m_leftBtn = (GButton)this.GetChildAt(10);
		}
	}
}