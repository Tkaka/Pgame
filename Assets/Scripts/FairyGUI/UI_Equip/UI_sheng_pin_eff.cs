/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_sheng_pin_eff : GComponent
	{
		public Transition m_anim;

		public const string URL = "ui://8u3gv94nrsct1u";

		public static UI_sheng_pin_eff CreateInstance()
		{
			return (UI_sheng_pin_eff)UIPackage.CreateObject("UI_Equip","sheng_pin_eff");
		}

		public UI_sheng_pin_eff()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_anim = this.GetTransitionAt(0);
		}
	}
}