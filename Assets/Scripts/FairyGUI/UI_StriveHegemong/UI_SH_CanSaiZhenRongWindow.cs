/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_CanSaiZhenRongWindow : GComponent
	{
		public GTextField m_name;
		public GTextField m_dengji;
		public GTextField m_zhuangtai;
		public GTextField m_zhanji;

		public const string URL = "ui://yjvxfmwogk691z";

		public static UI_SH_CanSaiZhenRongWindow CreateInstance()
		{
			return (UI_SH_CanSaiZhenRongWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_CanSaiZhenRongWindow");
		}

		public UI_SH_CanSaiZhenRongWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_name = (GTextField)this.GetChildAt(3);
			m_dengji = (GTextField)this.GetChildAt(5);
			m_zhuangtai = (GTextField)this.GetChildAt(6);
			m_zhanji = (GTextField)this.GetChildAt(7);
		}
	}
}