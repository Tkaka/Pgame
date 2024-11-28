/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_objEquip : GButton
	{
		public GTextField m_title1;

		public const string URL = "ui://w9mypx6jhona1i";

		public static UI_objEquip CreateInstance()
		{
			return (UI_objEquip)UIPackage.CreateObject("UI_Shop","objEquip");
		}

		public UI_objEquip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title1 = (GTextField)this.GetChildAt(3);
		}
	}
}