/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_equipAwakeBtn : GButton
	{
		public GImage m_m_redPoint;
		public GComponent m_lockGroup;
		public GTextField m_title1;

		public const string URL = "ui://w9mypx6jb48q1h";

		public static UI_equipAwakeBtn CreateInstance()
		{
			return (UI_equipAwakeBtn)UIPackage.CreateObject("UI_Shop","equipAwakeBtn");
		}

		public UI_equipAwakeBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_m_redPoint = (GImage)this.GetChildAt(3);
			m_lockGroup = (GComponent)this.GetChildAt(4);
			m_title1 = (GTextField)this.GetChildAt(5);
		}
	}
}