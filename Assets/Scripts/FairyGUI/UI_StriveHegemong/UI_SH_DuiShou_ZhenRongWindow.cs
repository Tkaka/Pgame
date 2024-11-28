/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_DuiShou_ZhenRongWindow : GComponent
	{
		public GList m_zhenrongList;
		public GTextField m_name;
		public GTextField m_dengji;
		public GTextField m_zhuangtai;
		public GTextField m_zhanji;
		public GButton m_CloseBtn;

		public const string URL = "ui://yjvxfmwoe8xk1p";

		public static UI_SH_DuiShou_ZhenRongWindow CreateInstance()
		{
			return (UI_SH_DuiShou_ZhenRongWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_DuiShou_ZhenRongWindow");
		}

		public UI_SH_DuiShou_ZhenRongWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_zhenrongList = (GList)this.GetChildAt(3);
			m_name = (GTextField)this.GetChildAt(5);
			m_dengji = (GTextField)this.GetChildAt(7);
			m_zhuangtai = (GTextField)this.GetChildAt(8);
			m_zhanji = (GTextField)this.GetChildAt(9);
			m_CloseBtn = (GButton)this.GetChildAt(10);
		}
	}
}