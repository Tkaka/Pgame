/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnJump : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://51gazvjd7igt1i";

		public static UI_btnJump CreateInstance()
		{
			return (UI_btnJump)UIPackage.CreateObject("UI_Chat","btnJump");
		}

		public UI_btnJump()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}