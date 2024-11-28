/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_JiangPinZhanShiWindow : GComponent
	{
		public GList m_JiangPinList;
		public GGraph m_BeiJingChuFa;
		public GTextField m_BiaoTi;
		public GTextField m_JiaGe;
		public UI_ZaiLaiYiCiBtn m_ZaiLaiYiCiBtn;
		public GButton m_QueDingBtn;
		public GTextField m_ShengYu;
		public GGroup m_ShiLianChou;
		public GGroup m_DanCiChou;
		public GGroup m_BaiLianChou;
		public GLoader m_priceLoader;
		public GGroup m_all;
		public Transition m_FaPai;
		public Transition m_DanChou;
		public Transition m_BaiLianChou_2;

		public const string URL = "ui://zy7t2yegw9w21c";

		public static UI_JiangPinZhanShiWindow CreateInstance()
		{
			return (UI_JiangPinZhanShiWindow)UIPackage.CreateObject("UI_DrawCard","JiangPinZhanShiWindow");
		}

		public UI_JiangPinZhanShiWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_JiangPinList = (GList)this.GetChildAt(1);
			m_BeiJingChuFa = (GGraph)this.GetChildAt(2);
			m_BiaoTi = (GTextField)this.GetChildAt(3);
			m_JiaGe = (GTextField)this.GetChildAt(4);
			m_ZaiLaiYiCiBtn = (UI_ZaiLaiYiCiBtn)this.GetChildAt(5);
			m_QueDingBtn = (GButton)this.GetChildAt(6);
			m_ShengYu = (GTextField)this.GetChildAt(7);
			m_ShiLianChou = (GGroup)this.GetChildAt(28);
			m_DanCiChou = (GGroup)this.GetChildAt(32);
			m_BaiLianChou = (GGroup)this.GetChildAt(43);
			m_priceLoader = (GLoader)this.GetChildAt(44);
			m_all = (GGroup)this.GetChildAt(45);
			m_FaPai = this.GetTransitionAt(0);
			m_DanChou = this.GetTransitionAt(1);
			m_BaiLianChou_2 = this.GetTransitionAt(2);
		}
	}
}