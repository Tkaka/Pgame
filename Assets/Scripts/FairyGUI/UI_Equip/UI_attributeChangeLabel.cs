/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_attributeChangeLabel : GComponent
	{
		public GTextField m_nameLabel;
		public GTextField m_oldValue;
		public GTextField m_newValue;

		public const string URL = "ui://8u3gv94nqwou1d";

		public static UI_attributeChangeLabel CreateInstance()
		{
			return (UI_attributeChangeLabel)UIPackage.CreateObject("UI_Equip","attributeChangeLabel");
		}

		public UI_attributeChangeLabel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_nameLabel = (GTextField)this.GetChildAt(0);
			m_oldValue = (GTextField)this.GetChildAt(1);
			m_newValue = (GTextField)this.GetChildAt(2);
		}
	}
}