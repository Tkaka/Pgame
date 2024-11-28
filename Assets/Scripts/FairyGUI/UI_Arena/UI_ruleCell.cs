/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ruleCell : GComponent
	{
		public GImage m_ruleCell;
		public GTextField m_txtTitle;

		public const string URL = "ui://3xs7lfyxh53e2i";

		public static UI_ruleCell CreateInstance()
		{
			return (UI_ruleCell)UIPackage.CreateObject("UI_Arena","ruleCell");
		}

		public UI_ruleCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ruleCell = (GImage)this.GetChildAt(0);
			m_txtTitle = (GTextField)this.GetChildAt(1);
		}
	}
}