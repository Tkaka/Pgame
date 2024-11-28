/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_JoinGuildWnd : GComponent
	{
		public GList m_guildList;
		public GComponent m_btnRight;
		public GComponent m_btnLeft;
		public GTextField m_txtPage;
		public UI_btnFastJoin m_btnFastJoin;

		public const string URL = "ui://oe7ras64f1jg2v";

		public static UI_JoinGuildWnd CreateInstance()
		{
			return (UI_JoinGuildWnd)UIPackage.CreateObject("UI_Guild","JoinGuildWnd");
		}

		public UI_JoinGuildWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_guildList = (GList)this.GetChildAt(0);
			m_btnRight = (GComponent)this.GetChildAt(6);
			m_btnLeft = (GComponent)this.GetChildAt(7);
			m_txtPage = (GTextField)this.GetChildAt(8);
			m_btnFastJoin = (UI_btnFastJoin)this.GetChildAt(9);
		}
	}
}