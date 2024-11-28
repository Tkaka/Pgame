/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objPropertyCel3 : GComponent
	{
		public GTextField m_txtPropertyName;
		public GTextField m_txtPropertyValue;
		public GTextField m_txtPropertyNextValue;

		public const string URL = "ui://vexa0xrygc7j1a";

		public static UI_objPropertyCel3 CreateInstance()
		{
			return (UI_objPropertyCel3)UIPackage.CreateObject("UI_AoYi","objPropertyCel3");
		}

		public UI_objPropertyCel3()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtPropertyName = (GTextField)this.GetChildAt(0);
			m_txtPropertyValue = (GTextField)this.GetChildAt(1);
			m_txtPropertyNextValue = (GTextField)this.GetChildAt(3);
		}
	}
}