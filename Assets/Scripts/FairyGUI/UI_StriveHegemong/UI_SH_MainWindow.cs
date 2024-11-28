/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_MainWindow : GComponent
	{
		public Controller m_SH_Type;
		public UI_SH_ZS_NO m_SH_ZS_weikaizhan;
		public UI_SH_ZS_OFF m_SH_ZS_yikaizhan;
		public UI_SH_WoDeSaiCheng m_wodesaicheng;
		public GList m_HuiGuList;
		public GButton m_DuiHuanBtn;
		public GButton m_GuiZeBtn;
		public GButton m_PaiHangBtn;
		public GGraph m_HG_wodesaiceng;
		public GGraph m_HG_baqiangsaicheng;
		public GButton m_CloseBtn;
		public UI_SH_HG_baqiangJianKuang m_EigheFinal;
		public GButton m_chuangkouceshi;

		public const string URL = "ui://yjvxfmwon7xz0";

		public static UI_SH_MainWindow CreateInstance()
		{
			return (UI_SH_MainWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_MainWindow");
		}

		public UI_SH_MainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_SH_Type = this.GetControllerAt(0);
			m_SH_ZS_weikaizhan = (UI_SH_ZS_NO)this.GetChildAt(5);
			m_SH_ZS_yikaizhan = (UI_SH_ZS_OFF)this.GetChildAt(6);
			m_wodesaicheng = (UI_SH_WoDeSaiCheng)this.GetChildAt(7);
			m_HuiGuList = (GList)this.GetChildAt(8);
			m_DuiHuanBtn = (GButton)this.GetChildAt(9);
			m_GuiZeBtn = (GButton)this.GetChildAt(10);
			m_PaiHangBtn = (GButton)this.GetChildAt(11);
			m_HG_wodesaiceng = (GGraph)this.GetChildAt(12);
			m_HG_baqiangsaicheng = (GGraph)this.GetChildAt(13);
			m_CloseBtn = (GButton)this.GetChildAt(14);
			m_EigheFinal = (UI_SH_HG_baqiangJianKuang)this.GetChildAt(15);
			m_chuangkouceshi = (GButton)this.GetChildAt(16);
		}
	}
}