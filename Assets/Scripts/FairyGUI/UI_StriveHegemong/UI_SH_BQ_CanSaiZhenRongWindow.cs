/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_BQ_CanSaiZhenRongWindow : GComponent
	{
		public GTextField m_name;
		public GTextField m_dengji;
		public GTextField m_zhuangtai;
		public GTextField m_zhanji;
		public GList m_zhenrongList;
		public GButton m_CliseBtn;

		public const string URL = "ui://yjvxfmwogrok1k";

		public static UI_SH_BQ_CanSaiZhenRongWindow CreateInstance()
		{
			return (UI_SH_BQ_CanSaiZhenRongWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_BQ_CanSaiZhenRongWindow");
		}

		public UI_SH_BQ_CanSaiZhenRongWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_name = (GTextField)this.GetChildAt(3);
			m_dengji = (GTextField)this.GetChildAt(5);
			m_zhuangtai = (GTextField)this.GetChildAt(6);
			m_zhanji = (GTextField)this.GetChildAt(7);
			m_zhenrongList = (GList)this.GetChildAt(8);
			m_CliseBtn = (GButton)this.GetChildAt(9);
		}
	}
}