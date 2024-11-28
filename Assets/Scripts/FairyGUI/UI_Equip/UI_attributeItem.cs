/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_attributeItem : GComponent
	{
		public GTextField m_tip;
		public GTextField m_value;
		public Transition m_animation;

		public const string URL = "ui://8u3gv94nqwou1c";

		public static UI_attributeItem CreateInstance()
		{
			return (UI_attributeItem)UIPackage.CreateObject("UI_Equip","attributeItem");
		}

		public UI_attributeItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tip = (GTextField)this.GetChildAt(0);
			m_value = (GTextField)this.GetChildAt(1);
			m_animation = this.GetTransitionAt(0);
		}
	}
}