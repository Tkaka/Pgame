/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_quickJumpPopupView : GComponent
	{
		public GList m_quickJumpChaterList;
		public GButton m_closeBtn;

		public const string URL = "ui://z04ymz0ew50m25n";

		public static UI_quickJumpPopupView CreateInstance()
		{
			return (UI_quickJumpPopupView)UIPackage.CreateObject("UI_Level","quickJumpPopupView");
		}

		public UI_quickJumpPopupView()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_quickJumpChaterList = (GList)this.GetChildAt(4);
			m_closeBtn = (GButton)this.GetChildAt(5);
		}
	}
}