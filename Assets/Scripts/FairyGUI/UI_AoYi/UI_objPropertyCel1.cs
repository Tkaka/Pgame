/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objPropertyCel1 : GComponent
	{
		public GTextField m_txtPropertyName;
		public GTextField m_txtPropertyValue;

		public const string URL = "ui://vexa0xrycpnr6";

		public static UI_objPropertyCel1 CreateInstance()
		{
			return (UI_objPropertyCel1)UIPackage.CreateObject("UI_AoYi","objPropertyCel1");
		}

		public UI_objPropertyCel1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtPropertyName = (GTextField)this.GetChildAt(0);
			m_txtPropertyValue = (GTextField)this.GetChildAt(1);
		}
	}
}