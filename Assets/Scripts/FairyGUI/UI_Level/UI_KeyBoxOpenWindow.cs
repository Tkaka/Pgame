/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_KeyBoxOpenWindow : GComponent
	{
		public GGraph m_mask;
		public UI_keyBoxOpenPopupView m_popupView;

		public const string URL = "ui://z04ymz0e97kmv";

		public static UI_KeyBoxOpenWindow CreateInstance()
		{
			return (UI_KeyBoxOpenWindow)UIPackage.CreateObject("UI_Level","KeyBoxOpenWindow");
		}

		public UI_KeyBoxOpenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_keyBoxOpenPopupView)this.GetChildAt(1);
		}
	}
}