/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_GuildInfoCell : GComponent
	{
		public GLoader m_imgLevel;
		public GTextField m_txtRank;
		public GLoader m_imgBadge;
		public GTextField m_txtGuildType;
		public GTextField m_txtName;
		public GTextField m_txtGuildLevel;
		public GTextField m_txtMemberNum;
		public GTextField m_txtLimitLevel;
		public GTextField m_txtLimitType;
		public UI_btnJoin m_btnJoin;
		public GComponent m_objApplying;
		public GComponent m_objFull;

		public const string URL = "ui://oe7ras64f1jg2w";

		public static UI_GuildInfoCell CreateInstance()
		{
			return (UI_GuildInfoCell)UIPackage.CreateObject("UI_Guild","GuildInfoCell");
		}

		public UI_GuildInfoCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgLevel = (GLoader)this.GetChildAt(1);
			m_txtRank = (GTextField)this.GetChildAt(2);
			m_imgBadge = (GLoader)this.GetChildAt(3);
			m_txtGuildType = (GTextField)this.GetChildAt(5);
			m_txtName = (GTextField)this.GetChildAt(6);
			m_txtGuildLevel = (GTextField)this.GetChildAt(7);
			m_txtMemberNum = (GTextField)this.GetChildAt(8);
			m_txtLimitLevel = (GTextField)this.GetChildAt(9);
			m_txtLimitType = (GTextField)this.GetChildAt(10);
			m_btnJoin = (UI_btnJoin)this.GetChildAt(11);
			m_objApplying = (GComponent)this.GetChildAt(12);
			m_objFull = (GComponent)this.GetChildAt(13);
		}
	}
}