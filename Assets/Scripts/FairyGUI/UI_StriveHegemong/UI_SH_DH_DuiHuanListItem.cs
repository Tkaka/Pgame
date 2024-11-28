/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_DH_DuiHuanListItem : GComponent
	{
		public UI_SH_DH_DaoJuIcon m_DaiBi;
		public UI_SH_DH_DaoJuIcon m_DaoJu;
		public GButton m_DuiHuanBtn;

		public const string URL = "ui://yjvxfmwojdrg10";

		public static UI_SH_DH_DuiHuanListItem CreateInstance()
		{
			return (UI_SH_DH_DuiHuanListItem)UIPackage.CreateObject("UI_StriveHegemong","SH_DH_DuiHuanListItem");
		}

		public UI_SH_DH_DuiHuanListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_DaiBi = (UI_SH_DH_DaoJuIcon)this.GetChildAt(1);
			m_DaoJu = (UI_SH_DH_DaoJuIcon)this.GetChildAt(2);
			m_DuiHuanBtn = (GButton)this.GetChildAt(4);
		}
	}
}