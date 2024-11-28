/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_FuncTipWindow : GComponent
	{
		public UI_TipFuncView m_tip;
		public Transition m_t2;

		public const string URL = "ui://jdfufi06h2kp4p";

		public static UI_FuncTipWindow CreateInstance()
		{
			return (UI_FuncTipWindow)UIPackage.CreateObject("UI_MainCity","FuncTipWindow");
		}

		public UI_FuncTipWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tip = (UI_TipFuncView)this.GetChildAt(1);
			m_t2 = this.GetTransitionAt(0);
		}
	}
}