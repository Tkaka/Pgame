/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_Dropdown : GComboBox
	{
		public GButton m_button;

		public const string URL = "ui://oe7ras64qbwu1m";

		public static UI_Dropdown CreateInstance()
		{
			return (UI_Dropdown)UIPackage.CreateObject("UI_Guild","Dropdown");
		}

		public UI_Dropdown()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_button = (GButton)this.GetChildAt(0);
		}
	}
}