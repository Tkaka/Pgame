/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnLaBa : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://51gazvjd7igt18";

		public static UI_btnLaBa CreateInstance()
		{
			return (UI_btnLaBa)UIPackage.CreateObject("UI_Chat","btnLaBa");
		}

		public UI_btnLaBa()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}