/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_objItem : GButton
	{
		public GTextField m_title1;

		public const string URL = "ui://w9mypx6jhona1j";

		public static UI_objItem CreateInstance()
		{
			return (UI_objItem)UIPackage.CreateObject("UI_Shop","objItem");
		}

		public UI_objItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title1 = (GTextField)this.GetChildAt(3);
		}
	}
}