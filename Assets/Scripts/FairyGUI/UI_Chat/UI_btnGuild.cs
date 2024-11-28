/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnGuild : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://51gazvjd7igt15";

		public static UI_btnGuild CreateInstance()
		{
			return (UI_btnGuild)UIPackage.CreateObject("UI_Chat","btnGuild");
		}

		public UI_btnGuild()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}