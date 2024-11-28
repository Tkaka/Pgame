/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_Joystick : GComponent
	{
		public GGraph m_touchArea;
		public GImage m_bottom;
		public GImage m_center;

		public const string URL = "ui://jdfufi06ro1f6b";

		public static UI_Joystick CreateInstance()
		{
			return (UI_Joystick)UIPackage.CreateObject("UI_MainCity","Joystick");
		}

		public UI_Joystick()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_touchArea = (GGraph)this.GetChildAt(0);
			m_bottom = (GImage)this.GetChildAt(1);
			m_center = (GImage)this.GetChildAt(2);
		}
	}
}