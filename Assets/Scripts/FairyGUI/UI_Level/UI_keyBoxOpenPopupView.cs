/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_keyBoxOpenPopupView : GComponent
	{
		public GList m_chapterBoxList;
		public GButton m_receiveBtn;
		public GButton m_closeBtn;

		public const string URL = "ui://z04ymz0ew50m25l";

		public static UI_keyBoxOpenPopupView CreateInstance()
		{
			return (UI_keyBoxOpenPopupView)UIPackage.CreateObject("UI_Level","keyBoxOpenPopupView");
		}

		public UI_keyBoxOpenPopupView()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_chapterBoxList = (GList)this.GetChildAt(6);
			m_receiveBtn = (GButton)this.GetChildAt(7);
			m_closeBtn = (GButton)this.GetChildAt(8);
		}
	}
}