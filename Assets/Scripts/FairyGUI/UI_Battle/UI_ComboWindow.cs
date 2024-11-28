/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboWindow : GComponent
	{
		public GGraph m_toucher;
		public UI_ComboCircle m_comboCircle;

		public const string URL = "ui://028ppdzhg2mxa0";

		public static UI_ComboWindow CreateInstance()
		{
			return (UI_ComboWindow)UIPackage.CreateObject("UI_Battle","ComboWindow");
		}

		public UI_ComboWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_toucher = (GGraph)this.GetChildAt(0);
			m_comboCircle = (UI_ComboCircle)this.GetChildAt(1);
		}
	}
}