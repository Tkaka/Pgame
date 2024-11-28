/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboTip_6 : GComponent
	{
		public GTextInput m_comboTxt;
		public Transition m_t0;

		public const string URL = "ui://028ppdzhhlijag";

		public static UI_ComboTip_6 CreateInstance()
		{
			return (UI_ComboTip_6)UIPackage.CreateObject("UI_Battle","ComboTip_6");
		}

		public UI_ComboTip_6()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_comboTxt = (GTextInput)this.GetChildAt(2);
			m_t0 = this.GetTransitionAt(0);
		}
	}
}