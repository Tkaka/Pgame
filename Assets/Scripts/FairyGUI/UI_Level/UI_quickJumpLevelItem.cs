/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_quickJumpLevelItem : GComponent
	{
		public GLoader m_boardLoarder;
		public GTextField m_nameLable;
		public GList m_starList;
		public GButton m_fightBtn;
		public GLoader m_iconLoader;

		public const string URL = "ui://z04ymz0ekb9x1n";

		public static UI_quickJumpLevelItem CreateInstance()
		{
			return (UI_quickJumpLevelItem)UIPackage.CreateObject("UI_Level","quickJumpLevelItem");
		}

		public UI_quickJumpLevelItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoarder = (GLoader)this.GetChildAt(1);
			m_nameLable = (GTextField)this.GetChildAt(2);
			m_starList = (GList)this.GetChildAt(3);
			m_fightBtn = (GButton)this.GetChildAt(4);
			m_iconLoader = (GLoader)this.GetChildAt(5);
		}
	}
}