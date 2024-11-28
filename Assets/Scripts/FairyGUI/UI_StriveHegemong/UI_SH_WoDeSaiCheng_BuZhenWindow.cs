/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_WoDeSaiCheng_BuZhenWindow : GComponent
	{
		public GList m_ChongWuList;
		public GButton m_TuiChu;
		public GButton m_BaoCun;

		public const string URL = "ui://yjvxfmwodshf1t";

		public static UI_SH_WoDeSaiCheng_BuZhenWindow CreateInstance()
		{
			return (UI_SH_WoDeSaiCheng_BuZhenWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_WoDeSaiCheng_BuZhenWindow");
		}

		public UI_SH_WoDeSaiCheng_BuZhenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ChongWuList = (GList)this.GetChildAt(3);
			m_TuiChu = (GButton)this.GetChildAt(4);
			m_BaoCun = (GButton)this.GetChildAt(5);
		}
	}
}