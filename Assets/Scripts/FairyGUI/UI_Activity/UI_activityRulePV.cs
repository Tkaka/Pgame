/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_activityRulePV : GComponent
	{
		public GTextField m_typeLabel;
		public GButton m_forkBtn;
		public GTextField m_openTimeTipLabel;
		public GTextField m_openTimeLabel;
		public GTextField m_specialRuleTipLabel;
		public GTextField m_specialRuleLabel;

		public const string URL = "ui://zwmeip9up63y1m";

		public static UI_activityRulePV CreateInstance()
		{
			return (UI_activityRulePV)UIPackage.CreateObject("UI_Activity","activityRulePV");
		}

		public UI_activityRulePV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_typeLabel = (GTextField)this.GetChildAt(4);
			m_forkBtn = (GButton)this.GetChildAt(5);
			m_openTimeTipLabel = (GTextField)this.GetChildAt(8);
			m_openTimeLabel = (GTextField)this.GetChildAt(9);
			m_specialRuleTipLabel = (GTextField)this.GetChildAt(11);
			m_specialRuleLabel = (GTextField)this.GetChildAt(12);
		}
	}
}