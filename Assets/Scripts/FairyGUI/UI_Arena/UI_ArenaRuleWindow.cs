/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ArenaRuleWindow : GComponent
	{
		public GButton m_btnClose;
		public GList m_txtList;

		public const string URL = "ui://3xs7lfyxehrw2f";

		public static UI_ArenaRuleWindow CreateInstance()
		{
			return (UI_ArenaRuleWindow)UIPackage.CreateObject("UI_Arena","ArenaRuleWindow");
		}

		public UI_ArenaRuleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_txtList = (GList)this.GetChildAt(4);
		}
	}
}