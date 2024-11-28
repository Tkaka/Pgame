/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_SendEmailWnd : GComponent
	{
		public GButton m_btnClose;
		public GTextInput m_txtInput;
		public UI_btnSend m_btnSend;

		public const string URL = "ui://oe7ras64fde920";

		public static UI_SendEmailWnd CreateInstance()
		{
			return (UI_SendEmailWnd)UIPackage.CreateObject("UI_Guild","SendEmailWnd");
		}

		public UI_SendEmailWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_txtInput = (GTextInput)this.GetChildAt(4);
			m_btnSend = (UI_btnSend)this.GetChildAt(5);
		}
	}
}