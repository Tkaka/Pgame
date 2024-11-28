/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Top
{
	public partial class UI_Top_mainWindow : GComponent
	{
		public Controller m_type;
		public GButton m_HallBtn;
		public GButton m_guildBtn;
		public GButton m_QuanHuangBtn;
		public GList m_PaiHangList;
		public GTextField m_name;
		public GLoader m_Icon;
		public GTextField m_level;
		public GTextField m_GuilName;
		public GButton m_ChaKanBtn;
		public UI_Top_ListItem m_Rank_MyData;
		public UI_fengetiao m_FenGeXian;
		public UI_Top_GuildListItem m_Guild_MyData;
		public GGraph m_CloseBtn;

		public const string URL = "ui://y4tkaqbbjdpb0";

		public static UI_Top_mainWindow CreateInstance()
		{
			return (UI_Top_mainWindow)UIPackage.CreateObject("UI_Top","Top_mainWindow");
		}

		public UI_Top_mainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_type = this.GetControllerAt(0);
			m_HallBtn = (GButton)this.GetChildAt(2);
			m_guildBtn = (GButton)this.GetChildAt(4);
			m_QuanHuangBtn = (GButton)this.GetChildAt(5);
			m_PaiHangList = (GList)this.GetChildAt(10);
			m_name = (GTextField)this.GetChildAt(11);
			m_Icon = (GLoader)this.GetChildAt(12);
			m_level = (GTextField)this.GetChildAt(13);
			m_GuilName = (GTextField)this.GetChildAt(14);
			m_ChaKanBtn = (GButton)this.GetChildAt(15);
			m_Rank_MyData = (UI_Top_ListItem)this.GetChildAt(16);
			m_FenGeXian = (UI_fengetiao)this.GetChildAt(17);
			m_Guild_MyData = (UI_Top_GuildListItem)this.GetChildAt(18);
			m_CloseBtn = (GGraph)this.GetChildAt(21);
		}
	}
}