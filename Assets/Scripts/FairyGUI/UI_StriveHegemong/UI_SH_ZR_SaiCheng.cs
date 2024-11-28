/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ZR_SaiCheng : GComponent
	{
		public UI_SH_BM_ListItem m_juese;
		public GTextField m_Changci;
		public GTextField m_xianshouzhi;
		public GTextField m_ZhanLiZhi;

		public const string URL = "ui://yjvxfmwogrok1m";

		public static UI_SH_ZR_SaiCheng CreateInstance()
		{
			return (UI_SH_ZR_SaiCheng)UIPackage.CreateObject("UI_StriveHegemong","SH_ZR_SaiCheng");
		}

		public UI_SH_ZR_SaiCheng()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_juese = (UI_SH_BM_ListItem)this.GetChildAt(1);
			m_Changci = (GTextField)this.GetChildAt(2);
			m_xianshouzhi = (GTextField)this.GetChildAt(4);
			m_ZhanLiZhi = (GTextField)this.GetChildAt(5);
		}
	}
}