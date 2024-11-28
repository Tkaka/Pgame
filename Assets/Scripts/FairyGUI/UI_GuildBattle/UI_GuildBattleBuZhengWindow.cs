/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_GuildBattleBuZhengWindow : GComponent
	{
		public Controller m_ctrl;
		public GTextField m_limitTipLabel;
		public GList m_buZhenList;
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;
		public GList m_petList;
		public GComponent m_commonTop;

		public const string URL = "ui://xj95784rvu1w29";

		public static UI_GuildBattleBuZhengWindow CreateInstance()
		{
			return (UI_GuildBattleBuZhengWindow)UIPackage.CreateObject("UI_GuildBattle","GuildBattleBuZhengWindow");
		}

		public UI_GuildBattleBuZhengWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_limitTipLabel = (GTextField)this.GetChildAt(12);
			m_buZhenList = (GList)this.GetChildAt(13);
			m_switchLeftBtn = (GButton)this.GetChildAt(14);
			m_switchRightBtn = (GButton)this.GetChildAt(15);
			m_petList = (GList)this.GetChildAt(16);
			m_commonTop = (GComponent)this.GetChildAt(17);
		}
	}
}