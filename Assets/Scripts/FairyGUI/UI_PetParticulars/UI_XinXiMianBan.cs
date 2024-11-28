/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_XinXiMianBan : GComponent
	{
		public GTextField m_DengJi;
		public GTextField m_ZiZhi;
		public GTextField m_GongJiLi;
		public GTextField m_FangYuLi;
		public GProgressBar m_SuiPianJinDu;
		public GImage m_SuiPianTuBiao;
		public GTextField m_jindu;
		public GGroup m_SuiPianJinDuTiao;
		public GLoader m_ZhanDouLiTuBiao;
		public GTextField m_ZhanDouLizhi;
		public GGroup m_ZhanLi;
		public GTextField m_ShengMingZhi;
		public UI_ChaKanXiangQingBtn m_XiangQingBtn;

		public const string URL = "ui://rn5o3g4tfzr69";

		public static UI_XinXiMianBan CreateInstance()
		{
			return (UI_XinXiMianBan)UIPackage.CreateObject("UI_PetParticulars","XinXiMianBan");
		}

		public UI_XinXiMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_DengJi = (GTextField)this.GetChildAt(7);
			m_ZiZhi = (GTextField)this.GetChildAt(9);
			m_GongJiLi = (GTextField)this.GetChildAt(11);
			m_FangYuLi = (GTextField)this.GetChildAt(13);
			m_SuiPianJinDu = (GProgressBar)this.GetChildAt(16);
			m_SuiPianTuBiao = (GImage)this.GetChildAt(17);
			m_jindu = (GTextField)this.GetChildAt(18);
			m_SuiPianJinDuTiao = (GGroup)this.GetChildAt(19);
			m_ZhanDouLiTuBiao = (GLoader)this.GetChildAt(20);
			m_ZhanDouLizhi = (GTextField)this.GetChildAt(21);
			m_ZhanLi = (GGroup)this.GetChildAt(22);
			m_ShengMingZhi = (GTextField)this.GetChildAt(23);
			m_XiangQingBtn = (UI_ChaKanXiangQingBtn)this.GetChildAt(24);
		}
	}
}