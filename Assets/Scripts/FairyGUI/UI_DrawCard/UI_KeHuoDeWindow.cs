/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_KeHuoDeWindow : GComponent
	{
		public Controller m_ChongWujiangLi;
		public UI_XianShiLieBiao m_JiangLiList;
		public GButton m_CloseBtn;
		public GButton m_JinBiJiangLi;
		public GGroup m_ZuanShiJiangLi;

		public const string URL = "ui://zy7t2yeggkzx1k";

		public static UI_KeHuoDeWindow CreateInstance()
		{
			return (UI_KeHuoDeWindow)UIPackage.CreateObject("UI_DrawCard","KeHuoDeWindow");
		}

		public UI_KeHuoDeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ChongWujiangLi = this.GetControllerAt(0);
			m_JiangLiList = (UI_XianShiLieBiao)this.GetChildAt(7);
			m_CloseBtn = (GButton)this.GetChildAt(8);
			m_JinBiJiangLi = (GButton)this.GetChildAt(9);
			m_ZuanShiJiangLi = (GGroup)this.GetChildAt(15);
		}
	}
}