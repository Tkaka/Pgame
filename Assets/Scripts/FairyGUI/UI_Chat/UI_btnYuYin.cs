/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnYuYin : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://51gazvjd7igtv";

		public static UI_btnYuYin CreateInstance()
		{
			return (UI_btnYuYin)UIPackage.CreateObject("UI_Chat","btnYuYin");
		}

		public UI_btnYuYin()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}