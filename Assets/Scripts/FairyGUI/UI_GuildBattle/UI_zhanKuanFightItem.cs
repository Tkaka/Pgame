/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_zhanKuanFightItem : GComponent
	{
		public UI_zhanKuanRoleItem m_leftRoleItem;
		public UI_zhanKuanRoleItem m_rightRoleItem;
		public GTextField m_changCiNum;
		public GButton m_luXiangBtn;

		public const string URL = "ui://xj95784riouo2v";

		public static UI_zhanKuanFightItem CreateInstance()
		{
			return (UI_zhanKuanFightItem)UIPackage.CreateObject("UI_GuildBattle","zhanKuanFightItem");
		}

		public UI_zhanKuanFightItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_leftRoleItem = (UI_zhanKuanRoleItem)this.GetChildAt(1);
			m_rightRoleItem = (UI_zhanKuanRoleItem)this.GetChildAt(2);
			m_changCiNum = (GTextField)this.GetChildAt(4);
			m_luXiangBtn = (GButton)this.GetChildAt(5);
		}
	}
}