/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ZR_DianFen : GComponent
	{
		public GTextField m_name;
		public GTextField m_One_ZL;
		public GTextField m_Two_ZL;
		public GTextField m_Three_Zl;
		public GTextField m_XianShouZhi;
		public GGraph m_ChuMo_one;
		public GGraph m_ChuMo_two;
		public GGraph m_ChuMo_three;
		public UI_SH_BM_ListItem m_One;
		public UI_SH_BM_ListItem m_Tow;
		public UI_SH_BM_ListItem m_Three;

		public const string URL = "ui://yjvxfmwogrok1l";

		public static UI_SH_ZR_DianFen CreateInstance()
		{
			return (UI_SH_ZR_DianFen)UIPackage.CreateObject("UI_StriveHegemong","SH_ZR_DianFen");
		}

		public UI_SH_ZR_DianFen()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_name = (GTextField)this.GetChildAt(1);
			m_One_ZL = (GTextField)this.GetChildAt(2);
			m_Two_ZL = (GTextField)this.GetChildAt(3);
			m_Three_Zl = (GTextField)this.GetChildAt(4);
			m_XianShouZhi = (GTextField)this.GetChildAt(5);
			m_ChuMo_one = (GGraph)this.GetChildAt(6);
			m_ChuMo_two = (GGraph)this.GetChildAt(7);
			m_ChuMo_three = (GGraph)this.GetChildAt(8);
			m_One = (UI_SH_BM_ListItem)this.GetChildAt(9);
			m_Tow = (UI_SH_BM_ListItem)this.GetChildAt(10);
			m_Three = (UI_SH_BM_ListItem)this.GetChildAt(11);
		}
	}
}