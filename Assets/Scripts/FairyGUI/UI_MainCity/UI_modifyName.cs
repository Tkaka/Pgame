/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_modifyName : GButton
	{
		public GTextField m_txt;

		public const string URL = "ui://jdfufi06kho74s";

		public static UI_modifyName CreateInstance()
		{
			return (UI_modifyName)UIPackage.CreateObject("UI_MainCity","modifyName");
		}

		public UI_modifyName()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txt = (GTextField)this.GetChildAt(1);
		}
	}
}