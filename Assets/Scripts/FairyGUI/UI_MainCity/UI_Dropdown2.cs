/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_Dropdown2 : GComponent
	{
		public GList m_list;

		public const string URL = "ui://jdfufi06ro1f5w";

		public static UI_Dropdown2 CreateInstance()
		{
			return (UI_Dropdown2)UIPackage.CreateObject("UI_MainCity","Dropdown2");
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