/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_StarNum : GComponent
	{
		public GImage m_star1;
		public GImage m_star2;
		public GImage m_star3;

		public const string URL = "ui://z04ymz0erpuw1g";

		public static UI_StarNum CreateInstance()
		{
			return (UI_StarNum)UIPackage.CreateObject("UI_Level","StarNum");
		}

		public UI_StarNum()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_star1 = (GImage)this.GetChildAt(3);
			m_star2 = (GImage)this.GetChildAt(4);
			m_star3 = (GImage)this.GetChildAt(5);
		}
	}
}