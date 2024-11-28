/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_ModifyNoticeWnd : GComponent
	{
		public GButton m_btnClose;
		public GTextInput m_txtInput;
		public UI_btnOk m_btnOk;

		public const string URL = "ui://oe7ras64105rb3i";

		public static UI_ModifyNoticeWnd CreateInstance()
		{
			return (UI_ModifyNoticeWnd)UIPackage.CreateObject("UI_Guild","ModifyNoticeWnd");
		}

		public UI_ModifyNoticeWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_txtInput = (GTextInput)this.GetChildAt(4);
			m_btnOk = (UI_btnOk)this.GetChildAt(6);
		}
	}
}