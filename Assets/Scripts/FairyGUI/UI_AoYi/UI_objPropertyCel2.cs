/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objPropertyCel2 : GComponent
	{
		public GTextField m_txtProperty;

		public const string URL = "ui://vexa0xrycpnr7";

		public static UI_objPropertyCel2 CreateInstance()
		{
			return (UI_objPropertyCel2)UIPackage.CreateObject("UI_AoYi","objPropertyCel2");
		}

		public UI_objPropertyCel2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtProperty = (GTextField)this.GetChildAt(0);
		}
	}
}