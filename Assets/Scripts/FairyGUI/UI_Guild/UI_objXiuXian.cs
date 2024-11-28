/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objXiuXian : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64f1jg34";

		public static UI_objXiuXian CreateInstance()
		{
			return (UI_objXiuXian)UIPackage.CreateObject("UI_Guild","objXiuXian");
		}

		public UI_objXiuXian()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}