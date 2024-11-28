/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboEvaluation_zi : GComponent
	{
		public Transition m_GO;

		public const string URL = "ui://028ppdzhhorbb5";

		public static UI_ComboEvaluation_zi CreateInstance()
		{
			return (UI_ComboEvaluation_zi)UIPackage.CreateObject("UI_Battle","ComboEvaluation_zi");
		}

		public UI_ComboEvaluation_zi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GO = this.GetTransitionAt(0);
		}
	}
}