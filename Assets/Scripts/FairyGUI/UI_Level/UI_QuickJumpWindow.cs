/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_QuickJumpWindow : GComponent
	{
		public GGraph m_mask;
		public UI_quickJumpPopupView m_popupView;

		public const string URL = "ui://z04ymz0ekb9x1k";

		public static UI_QuickJumpWindow CreateInstance()
		{
			return (UI_QuickJumpWindow)UIPackage.CreateObject("UI_Level","QuickJumpWindow");
		}

		public UI_QuickJumpWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_quickJumpPopupView)this.GetChildAt(1);
		}
	}
}