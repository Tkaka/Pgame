/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboTip : GComponent
	{
		public GTextInput m_comboTxt;
		public GLoader m_numLoader;
		public Transition m_t0;

		public const string URL = "ui://028ppdzhuspgav";

		public static UI_ComboTip CreateInstance()
		{
			return (UI_ComboTip)UIPackage.CreateObject("UI_Battle","ComboTip");
		}

		public UI_ComboTip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_comboTxt = (GTextInput)this.GetChildAt(2);
			m_numLoader = (GLoader)this.GetChildAt(5);
			m_t0 = this.GetTransitionAt(0);
		}
	}
}