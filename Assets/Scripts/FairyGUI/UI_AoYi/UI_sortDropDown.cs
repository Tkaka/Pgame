/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_sortDropDown : GComboBox
	{
		public GTextField m_lab;

		public const string URL = "ui://vexa0xryh9lho";

		public static UI_sortDropDown CreateInstance()
		{
			return (UI_sortDropDown)UIPackage.CreateObject("UI_AoYi","sortDropDown");
		}

		public UI_sortDropDown()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lab = (GTextField)this.GetChildAt(4);
		}
	}
}