/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnTeam : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://51gazvjd7igt17";

		public static UI_btnTeam CreateInstance()
		{
			return (UI_btnTeam)UIPackage.CreateObject("UI_Chat","btnTeam");
		}

		public UI_btnTeam()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}