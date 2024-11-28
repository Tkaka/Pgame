/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_FindGuildWnd : GComponent
	{
		public GTextInput m_txtGuildName;
		public GTextField m_txtCondition;
		public UI_btnFind m_btnFind;
		public GList m_guildList;

		public const string URL = "ui://oe7ras64f1jg36";

		public static UI_FindGuildWnd CreateInstance()
		{
			return (UI_FindGuildWnd)UIPackage.CreateObject("UI_Guild","FindGuildWnd");
		}

		public UI_FindGuildWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtGuildName = (GTextInput)this.GetChildAt(2);
			m_txtCondition = (GTextField)this.GetChildAt(3);
			m_btnFind = (UI_btnFind)this.GetChildAt(4);
			m_guildList = (GList)this.GetChildAt(5);
		}
	}
}