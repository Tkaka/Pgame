/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_DrawCardWindow : GComponent
	{
		public GComponent m_taitou;
		public GGraph m_ZuanShiZhaoHuan;
		public GButton m_zuanshichoujiangBtn;
		public GGraph m_JinBiZhaoHuan;
		public GButton m_jinbuchoujiangBtn;
		public GGroup m_all;
		public Transition m_XiaoHuPaAction;
		public Transition m_DaHuPaAction;

		public const string URL = "ui://zy7t2yegub59x";

		public static UI_DrawCardWindow CreateInstance()
		{
			return (UI_DrawCardWindow)UIPackage.CreateObject("UI_DrawCard","DrawCardWindow");
		}

		public UI_DrawCardWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_taitou = (GComponent)this.GetChildAt(0);
			m_ZuanShiZhaoHuan = (GGraph)this.GetChildAt(1);
			m_zuanshichoujiangBtn = (GButton)this.GetChildAt(2);
			m_JinBiZhaoHuan = (GGraph)this.GetChildAt(4);
			m_jinbuchoujiangBtn = (GButton)this.GetChildAt(5);
			m_all = (GGroup)this.GetChildAt(12);
			m_XiaoHuPaAction = this.GetTransitionAt(0);
			m_DaHuPaAction = this.GetTransitionAt(1);
		}
	}
}