/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_UltemateTrainRuleWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_ruleList;
		public GButton m_closeBtn;

		public const string URL = "ui://1wdkrtiuw0huc";

		public static UI_UltemateTrainRuleWindow CreateInstance()
		{
			return (UI_UltemateTrainRuleWindow)UIPackage.CreateObject("UI_UltemateTrain","UltemateTrainRuleWindow");
		}

		public UI_UltemateTrainRuleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_ruleList = (GList)this.GetChildAt(7);
			m_closeBtn = (GButton)this.GetChildAt(8);
		}
	}
}