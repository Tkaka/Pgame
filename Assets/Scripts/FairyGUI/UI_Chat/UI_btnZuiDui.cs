/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnZuiDui : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://51gazvjd7igt16";

		public static UI_btnZuiDui CreateInstance()
		{
			return (UI_btnZuiDui)UIPackage.CreateObject("UI_Chat","btnZuiDui");
		}

		public UI_btnZuiDui()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}