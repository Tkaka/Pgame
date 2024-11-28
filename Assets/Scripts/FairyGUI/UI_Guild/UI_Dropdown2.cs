/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_Dropdown2 : GComponent
	{
		public GList m_list;

		public const string URL = "ui://oe7ras64qbwu1n";

		public static UI_Dropdown2 CreateInstance()
		{
			return (UI_Dropdown2)UIPackage.CreateObject("UI_Guild","Dropdown2");
		}

		public UI_Dropdown2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_list = (GList)this.GetChildAt(1);
		}
	}
}