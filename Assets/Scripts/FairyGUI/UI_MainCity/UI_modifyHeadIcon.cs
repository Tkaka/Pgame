/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_modifyHeadIcon : GButton
	{
		public GTextField m_txt;

		public const string URL = "ui://jdfufi06kho74t";

		public static UI_modifyHeadIcon CreateInstance()
		{
			return (UI_modifyHeadIcon)UIPackage.CreateObject("UI_MainCity","modifyHeadIcon");
		}

		public UI_modifyHeadIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txt = (GTextField)this.GetChildAt(1);
		}
	}
}