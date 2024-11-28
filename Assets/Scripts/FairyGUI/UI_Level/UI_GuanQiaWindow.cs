/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_GuanQiaWindow : GComponent
	{
		public GGraph m_btnBgMask;
		public UI_guanQiaPopupView m_guanQiaPopupView;

		public const string URL = "ui://z04ymz0emgd610";

		public static UI_GuanQiaWindow CreateInstance()
		{
			return (UI_GuanQiaWindow)UIPackage.CreateObject("UI_Level","GuanQiaWindow");
		}

		public UI_GuanQiaWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnBgMask = (GGraph)this.GetChildAt(0);
			m_guanQiaPopupView = (UI_guanQiaPopupView)this.GetChildAt(1);
		}
	}
}