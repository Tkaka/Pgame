/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnHall : GComponent
	{
		public GImage m_imgRed;

		public const string URL = "ui://oe7ras64qbwu4";

		public static UI_btnHall CreateInstance()
		{
			return (UI_btnHall)UIPackage.CreateObject("UI_Guild","btnHall");
		}

		public UI_btnHall()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgRed = (GImage)this.GetChildAt(2);
		}
	}
}