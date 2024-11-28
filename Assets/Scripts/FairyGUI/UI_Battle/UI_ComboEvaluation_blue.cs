/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboEvaluation_blue : GComponent
	{
		public Transition m_GO;

		public const string URL = "ui://028ppdzhhorbbb";

		public static UI_ComboEvaluation_blue CreateInstance()
		{
			return (UI_ComboEvaluation_blue)UIPackage.CreateObject("UI_Battle","ComboEvaluation_blue");
		}

		public UI_ComboEvaluation_blue()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GO = this.GetTransitionAt(0);
		}
	}
}