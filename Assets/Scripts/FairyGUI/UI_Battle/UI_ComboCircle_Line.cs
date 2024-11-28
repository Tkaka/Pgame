/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboCircle_Line : GComponent
	{
		public Transition m_GO;

		public const string URL = "ui://028ppdzhhorbb1";

		public static UI_ComboCircle_Line CreateInstance()
		{
			return (UI_ComboCircle_Line)UIPackage.CreateObject("UI_Battle","ComboCircle_Line");
		}

		public UI_ComboCircle_Line()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GO = this.GetTransitionAt(0);
		}
	}
}