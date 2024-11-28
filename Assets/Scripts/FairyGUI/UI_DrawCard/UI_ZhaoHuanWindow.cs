/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_ZhaoHuanWindow : GComponent
	{
		public GComponent m_TaiTou;
		public UI_JinBiJiangLiYiLanBtn m_jinbiyilanBtn;
		public GButton m_zuanshiyilanBtn;
		public GList m_moshiList;
		public GGroup m_JinBiMiaoShu;
		public GTextField m_ZuanShiMiaoShu;
		public GGroup m_zujian;

		public const string URL = "ui://zy7t2yegw9w21a";

		public static UI_ZhaoHuanWindow CreateInstance()
		{
			return (UI_ZhaoHuanWindow)UIPackage.CreateObject("UI_DrawCard","ZhaoHuanWindow");
		}

		public UI_ZhaoHuanWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TaiTou = (GComponent)this.GetChildAt(0);
			m_jinbiyilanBtn = (UI_JinBiJiangLiYiLanBtn)this.GetChildAt(1);
			m_zuanshiyilanBtn = (GButton)this.GetChildAt(2);
			m_moshiList = (GList)this.GetChildAt(3);
			m_JinBiMiaoShu = (GGroup)this.GetChildAt(6);
			m_ZuanShiMiaoShu = (GTextField)this.GetChildAt(7);
			m_zujian = (GGroup)this.GetChildAt(8);
		}
	}
}