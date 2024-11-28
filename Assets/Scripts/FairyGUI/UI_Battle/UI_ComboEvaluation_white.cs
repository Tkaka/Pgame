/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboEvaluation_white : GComponent
	{
		public Transition m_GO;

		public const string URL = "ui://028ppdzhhorbbc";

		public static UI_ComboEvaluation_white CreateInstance()
		{
			return (UI_ComboEvaluation_white)UIPackage.CreateObject("UI_Battle","ComboEvaluation_white");
		}

		public UI_ComboEvaluation_white()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GO = this.GetTransitionAt(0);
		}
	}
}