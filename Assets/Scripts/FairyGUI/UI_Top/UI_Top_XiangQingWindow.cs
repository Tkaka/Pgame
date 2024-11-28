/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Top
{
	public partial class UI_Top_XiangQingWindow : GComponent
	{
		public GButton m_closeBtn;
		public GLoader m_guild_Icon;
		public GLoader m_guild_type;
		public GTextField m_guild_name;
		public GTextField m_guild_sir;
		public GTextField m_guild_rank;
		public GTextField m_guild_chengyuan;
		public GTextField m_guild_gonggao;

		public const string URL = "ui://y4tkaqbbkv1se";

		public static UI_Top_XiangQingWindow CreateInstance()
		{
			return (UI_Top_XiangQingWindow)UIPackage.CreateObject("UI_Top","Top_XiangQingWindow");
		}

		public UI_Top_XiangQingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GButton)this.GetChildAt(3);
			m_guild_Icon = (GLoader)this.GetChildAt(4);
			m_guild_type = (GLoader)this.GetChildAt(5);
			m_guild_name = (GTextField)this.GetChildAt(6);
			m_guild_sir = (GTextField)this.GetChildAt(7);
			m_guild_rank = (GTextField)this.GetChildAt(8);
			m_guild_chengyuan = (GTextField)this.GetChildAt(9);
			m_guild_gonggao = (GTextField)this.GetChildAt(10);
		}
	}
}