/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_RuleWindow : GComponent
	{
		public GList m_GuiZeList;
		public GButton m_CloseBtn;

		public const string URL = "ui://yjvxfmwoqz7a18";

		public static UI_SH_RuleWindow CreateInstance()
		{
			return (UI_SH_RuleWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_RuleWindow");
		}

		public UI_SH_RuleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GuiZeList = (GList)this.GetChildAt(3);
			m_CloseBtn = (GButton)this.GetChildAt(4);
		}
	}
}