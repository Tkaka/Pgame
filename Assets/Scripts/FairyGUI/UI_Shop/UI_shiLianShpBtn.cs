/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_shiLianShpBtn : GButton
	{
		public GComponent m_lockGroup;
		public GTextField m_title1;

		public const string URL = "ui://w9mypx6jb48q1e";

		public static UI_shiLianShpBtn CreateInstance()
		{
			return (UI_shiLianShpBtn)UIPackage.CreateObject("UI_Shop","shiLianShpBtn");
		}

		public UI_shiLianShpBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lockGroup = (GComponent)this.GetChildAt(3);
			m_title1 = (GTextField)this.GetChildAt(4);
		}
	}
}