/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_RuleDetailWindow : GComponent
	{
		public GGraph m_mask;
		public UI_activityRulePV m_popupView;

		public const string URL = "ui://zwmeip9ukrhb5";

		public static UI_RuleDetailWindow CreateInstance()
		{
			return (UI_RuleDetailWindow)UIPackage.CreateObject("UI_Activity","RuleDetailWindow");
		}

		public UI_RuleDetailWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_activityRulePV)this.GetChildAt(1);
		}
	}
}