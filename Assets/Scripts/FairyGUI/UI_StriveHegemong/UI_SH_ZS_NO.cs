/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ZS_NO : GComponent
	{
		public UI_SH_ZS_NO_role m_Two;
		public UI_SH_ZS_NO_role m_One;
		public UI_SH_ZS_NO_role m_Three;
		public GButton m_XiaZhuBtn;
		public GTextField m_RenShu;
		public UI_SH_BaoMingBtn m_BaoMingBtn;
		public GTextField m_ShiJian;
		public GButton m_JiaoXue;

		public const string URL = "ui://yjvxfmwon7xz7";

		public static UI_SH_ZS_NO CreateInstance()
		{
			return (UI_SH_ZS_NO)UIPackage.CreateObject("UI_StriveHegemong","SH_ZS_NO");
		}

		public UI_SH_ZS_NO()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Two = (UI_SH_ZS_NO_role)this.GetChildAt(1);
			m_One = (UI_SH_ZS_NO_role)this.GetChildAt(2);
			m_Three = (UI_SH_ZS_NO_role)this.GetChildAt(3);
			m_XiaZhuBtn = (GButton)this.GetChildAt(4);
			m_RenShu = (GTextField)this.GetChildAt(5);
			m_BaoMingBtn = (UI_SH_BaoMingBtn)this.GetChildAt(6);
			m_ShiJian = (GTextField)this.GetChildAt(7);
			m_JiaoXue = (GButton)this.GetChildAt(8);
		}
	}
}