/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_keyReceiveBtn : GButton
	{
		public GImage m_redPoint;

		public const string URL = "ui://z04ymz0e97kmk";

		public static UI_keyReceiveBtn CreateInstance()
		{
			return (UI_keyReceiveBtn)UIPackage.CreateObject("UI_Level","keyReceiveBtn");
		}

		public UI_keyReceiveBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_redPoint = (GImage)this.GetChildAt(1);
		}
	}
}