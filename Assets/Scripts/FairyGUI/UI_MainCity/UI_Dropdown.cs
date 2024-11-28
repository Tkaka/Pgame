/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_Dropdown : GComboBox
	{
		public GButton m_button;

		public const string URL = "ui://jdfufi06ro1f5v";

		public static UI_Dropdown CreateInstance()
		{
			return (UI_Dropdown)UIPackage.CreateObject("UI_MainCity","Dropdown");
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