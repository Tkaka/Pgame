/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_WoDeSaiCheng : GComponent
	{
		public GTextField m_ZhuTi;
		public GTextField m_ZhuTiXiaoGuo;
		public GTextField m_tongzhi;
		public GList m_SaiChengList;
		public GTextField m_JiFen;
		public GButton m_BuZhenBtn;
		public GTextField m_ZhanKuang;

		public const string URL = "ui://yjvxfmwon7xzg";

		public static UI_SH_WoDeSaiCheng CreateInstance()
		{
			return (UI_SH_WoDeSaiCheng)UIPackage.CreateObject("UI_StriveHegemong","SH_WoDeSaiCheng");
		}

		public UI_SH_WoDeSaiCheng()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ZhuTi = (GTextField)this.GetChildAt(5);
			m_ZhuTiXiaoGuo = (GTextField)this.GetChildAt(7);
			m_tongzhi = (GTextField)this.GetChildAt(8);
			m_SaiChengList = (GList)this.GetChildAt(9);
			m_JiFen = (GTextField)this.GetChildAt(10);
			m_BuZhenBtn = (GButton)this.GetChildAt(11);
			m_ZhanKuang = (GTextField)this.GetChildAt(12);
		}
	}
}