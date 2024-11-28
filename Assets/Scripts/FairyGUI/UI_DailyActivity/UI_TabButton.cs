/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_TabButton : GButton
	{
		public GTextField m_tabTxt1;
		public GLoader m_showIcon;
		public GTextField m_tabTxt2;
		public GLoader m_mark;
		public GImage m_xhd;

		public const string URL = "ui://0n5r1ymrjqqo1";

		public static UI_TabButton CreateInstance()
		{
			return (UI_TabButton)UIPackage.CreateObject("UI_DailyActivity","TabButton");
		}

		public UI_TabButton()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tabTxt1 = (GTextField)this.GetChildAt(2);
			m_showIcon = (GLoader)this.GetChildAt(3);
			m_tabTxt2 = (GTextField)this.GetChildAt(4);
			m_mark = (GLoader)this.GetChildAt(5);
			m_xhd = (GImage)this.GetChildAt(6);
		}
	}
}