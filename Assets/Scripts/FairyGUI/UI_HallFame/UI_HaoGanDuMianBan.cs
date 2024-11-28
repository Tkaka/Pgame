/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HaoGanDuMianBan : GComponent
	{
		public GImage m_BeiJing;
		public GButton m_HF_xiangqing;
		public UI_HF_HaoGanDengJi m_HF_dengji;
		public UI_HF_JiShu m_HF_JiShu;
		public GProgressBar m_HF_JinDuTiao;
		public GButton m_HF_jiacheng;
		public GTextField m_HF_XianShouZhi;
		public GTextField m_MiaoShu;
		public GList m_CateList;
		public GButton m_HF_LaiYuan;
		public GTextField m_HF_jindutiaotext;
		public GButton m_HF_haogantisheng;
		public GLoader m_OneCate;
		public GLoader m_TwoCate;
		public GLoader m_ThreeCate;
		public Transition m_One;
		public Transition m_Two;
		public Transition m_Three;

		public const string URL = "ui://yo5kunkik5ji6";

		public static UI_HaoGanDuMianBan CreateInstance()
		{
			return (UI_HaoGanDuMianBan)UIPackage.CreateObject("UI_HallFame","HaoGanDuMianBan");
		}

		public UI_HaoGanDuMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GImage)this.GetChildAt(0);
			m_HF_xiangqing = (GButton)this.GetChildAt(1);
			m_HF_dengji = (UI_HF_HaoGanDengJi)this.GetChildAt(4);
			m_HF_JiShu = (UI_HF_JiShu)this.GetChildAt(5);
			m_HF_JinDuTiao = (GProgressBar)this.GetChildAt(7);
			m_HF_jiacheng = (GButton)this.GetChildAt(8);
			m_HF_XianShouZhi = (GTextField)this.GetChildAt(10);
			m_MiaoShu = (GTextField)this.GetChildAt(12);
			m_CateList = (GList)this.GetChildAt(13);
			m_HF_LaiYuan = (GButton)this.GetChildAt(14);
			m_HF_jindutiaotext = (GTextField)this.GetChildAt(15);
			m_HF_haogantisheng = (GButton)this.GetChildAt(16);
			m_OneCate = (GLoader)this.GetChildAt(18);
			m_TwoCate = (GLoader)this.GetChildAt(19);
			m_ThreeCate = (GLoader)this.GetChildAt(20);
			m_One = this.GetTransitionAt(0);
			m_Two = this.GetTransitionAt(1);
			m_Three = this.GetTransitionAt(2);
		}
	}
}