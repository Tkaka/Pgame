/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_tabCell : GButton
	{
		public GTextField m_txtDescribe;
		public GImage m_imgRed;

		public const string URL = "ui://w9mypx6jyzqv3";

		public static UI_tabCell CreateInstance()
		{
			return (UI_tabCell)UIPackage.CreateObject("UI_Shop","tabCell");
		}

		public UI_tabCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDescribe = (GTextField)this.GetChildAt(2);
			m_imgRed = (GImage)this.GetChildAt(3);
		}
	}
}