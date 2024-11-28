/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_DC_Type : GComponent
	{
		public GImage m_beijing;
		public GTextField m_cishu;
		public GTextField m_jiage;
		public GLoader m_type_icon;
		public UI_GouMaiBaiCiBtn m_BaiCiBtn;
		public UI_GouMaiShiCiBtn m_ShiCiBtn;
		public UI_GouMaiYiCiBtn m_YiCiBtn;
		public GTextField m_cishuxianzhi;
		public GImage m_banjia_Icon;

		public const string URL = "ui://zy7t2yegci3626";

		public static UI_DC_Type CreateInstance()
		{
			return (UI_DC_Type)UIPackage.CreateObject("UI_DrawCard","DC_Type");
		}

		public UI_DC_Type()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_beijing = (GImage)this.GetChildAt(0);
			m_cishu = (GTextField)this.GetChildAt(1);
			m_jiage = (GTextField)this.GetChildAt(2);
			m_type_icon = (GLoader)this.GetChildAt(3);
			m_BaiCiBtn = (UI_GouMaiBaiCiBtn)this.GetChildAt(4);
			m_ShiCiBtn = (UI_GouMaiShiCiBtn)this.GetChildAt(5);
			m_YiCiBtn = (UI_GouMaiYiCiBtn)this.GetChildAt(6);
			m_cishuxianzhi = (GTextField)this.GetChildAt(7);
			m_banjia_Icon = (GImage)this.GetChildAt(8);
		}
	}
}