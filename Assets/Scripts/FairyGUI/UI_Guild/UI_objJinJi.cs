/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objJinJi : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64f1jg33";

		public static UI_objJinJi CreateInstance()
		{
			return (UI_objJinJi)UIPackage.CreateObject("UI_Guild","objJinJi");
		}

		public UI_objJinJi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}