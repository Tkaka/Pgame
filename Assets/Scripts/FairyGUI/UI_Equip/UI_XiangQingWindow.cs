/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_XiangQingWindow : GComponent
	{
		public GGraph m_BeiJing;
		public GButton m_CloseBtn;
		public GLoader m_pinjie;
		public GLoader m_TouXiang;
		public GTextField m_Name;
		public GTextField m_ShuLiang;
		public GProgressBar m_SuiPianJinDu;
		public GTextField m_MiaoShu;
		public GButton m_HeCheng;
		public GButton m_LaiYuanBtn;

		public const string URL = "ui://8u3gv94nt5fap";

		public static UI_XiangQingWindow CreateInstance()
		{
			return (UI_XiangQingWindow)UIPackage.CreateObject("UI_Equip","XiangQingWindow");
		}

		public UI_XiangQingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GGraph)this.GetChildAt(0);
			m_CloseBtn = (GButton)this.GetChildAt(6);
			m_pinjie = (GLoader)this.GetChildAt(8);
			m_TouXiang = (GLoader)this.GetChildAt(9);
			m_Name = (GTextField)this.GetChildAt(10);
			m_ShuLiang = (GTextField)this.GetChildAt(12);
			m_SuiPianJinDu = (GProgressBar)this.GetChildAt(13);
			m_MiaoShu = (GTextField)this.GetChildAt(14);
			m_HeCheng = (GButton)this.GetChildAt(15);
			m_LaiYuanBtn = (GButton)this.GetChildAt(16);
		}
	}
}