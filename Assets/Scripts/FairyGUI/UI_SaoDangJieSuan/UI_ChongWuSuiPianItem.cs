/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_ChongWuSuiPianItem : GComponent
	{
		public GLoader m_PingJieKuang;
		public GLoader m_TouXiangKuang;
		public GLoader m_SuiPianKuang;
		public GTextField m_txtNum;

		public const string URL = "ui://34cd5b4hevv8m";

		public static UI_ChongWuSuiPianItem CreateInstance()
		{
			return (UI_ChongWuSuiPianItem)UIPackage.CreateObject("UI_SaoDangJieSuan","ChongWuSuiPianItem");
		}

		public UI_ChongWuSuiPianItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_PingJieKuang = (GLoader)this.GetChildAt(0);
			m_TouXiangKuang = (GLoader)this.GetChildAt(1);
			m_SuiPianKuang = (GLoader)this.GetChildAt(2);
			m_txtNum = (GTextField)this.GetChildAt(3);
		}
	}
}