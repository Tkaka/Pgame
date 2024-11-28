/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_MainWindow : GComponent
	{
		public Controller m_type;
		public UI_GRE_SheTuanHongBao m_shetuanhongbao;
		public GButton m_guizeBtn;
		public GButton m_paihangBtn;
		public GTextField m_TiShi;
		public UI_GRE_QiangHongBao m_QiangHongBao;
		public UI_GRE_FaHongBao m_fahongbao;
		public GComponent m_taitou;

		public const string URL = "ui://r816m4tmfzr60";

		public static UI_GRE_MainWindow CreateInstance()
		{
			return (UI_GRE_MainWindow)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_MainWindow");
		}

		public UI_GRE_MainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_type = this.GetControllerAt(0);
			m_shetuanhongbao = (UI_GRE_SheTuanHongBao)this.GetChildAt(8);
			m_guizeBtn = (GButton)this.GetChildAt(9);
			m_paihangBtn = (GButton)this.GetChildAt(10);
			m_TiShi = (GTextField)this.GetChildAt(11);
			m_QiangHongBao = (UI_GRE_QiangHongBao)this.GetChildAt(12);
			m_fahongbao = (UI_GRE_FaHongBao)this.GetChildAt(13);
			m_taitou = (GComponent)this.GetChildAt(15);
		}
	}
}