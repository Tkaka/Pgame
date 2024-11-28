/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_GuildBossPassRewardWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_closeBtn;
		public GButton m_receiveBtn;
		public GList m_passBossList;

		public const string URL = "ui://u2d86ulcjg9f4";

		public static UI_GuildBossPassRewardWindow CreateInstance()
		{
			return (UI_GuildBossPassRewardWindow)UIPackage.CreateObject("UI_GuildBoss","GuildBossPassRewardWindow");
		}

		public UI_GuildBossPassRewardWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_closeBtn = (GButton)this.GetChildAt(5);
			m_receiveBtn = (GButton)this.GetChildAt(6);
			m_passBossList = (GList)this.GetChildAt(7);
		}
	}
}