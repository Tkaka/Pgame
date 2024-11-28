/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ExchangeWindow : GComponent
	{
		public GTextField m_OneNumber;
		public GTextField m_TowNumber;
		public GTextField m_ThreeNumber;
		public GList m_DuiHuanList;
		public GButton m_CloseBtn;
		public GTextField m_baoji;
		public GTextField m_number;
		public UI_SH_DH_DaoJuIcon m_duihuanItem;
		public GGroup m_duihuanchenggong;

		public const string URL = "ui://yjvxfmwojdrgz";

		public static UI_SH_ExchangeWindow CreateInstance()
		{
			return (UI_SH_ExchangeWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_ExchangeWindow");
		}

		public UI_SH_ExchangeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_OneNumber = (GTextField)this.GetChildAt(3);
			m_TowNumber = (GTextField)this.GetChildAt(4);
			m_ThreeNumber = (GTextField)this.GetChildAt(5);
			m_DuiHuanList = (GList)this.GetChildAt(6);
			m_CloseBtn = (GButton)this.GetChildAt(7);
			m_baoji = (GTextField)this.GetChildAt(10);
			m_number = (GTextField)this.GetChildAt(12);
			m_duihuanItem = (UI_SH_DH_DaoJuIcon)this.GetChildAt(14);
			m_duihuanchenggong = (GGroup)this.GetChildAt(15);
		}
	}
}