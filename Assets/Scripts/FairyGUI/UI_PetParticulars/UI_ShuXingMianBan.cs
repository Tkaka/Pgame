/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_ShuXingMianBan : GComponent
	{
		public GTextField m_MianShangLvZhi;
		public GTextField m_ShangHaiLvZhi;
		public GTextField m_GeDangQiangDuZhi;
		public GTextField m_PoJiLvZhi;
		public GTextField m_GeDangLvZhi;
		public GTextField m_BaoJiQiangDuZhi;
		public GTextField m_KangBaoLvZhi;
		public GTextField m_BaoJiLvZhi;
		public GTextField m_fangyu;
		public GTextField m_ShengMingZhi;
		public GTextField m_GongJiZhi;

		public const string URL = "ui://rn5o3g4tfzr68";

		public static UI_ShuXingMianBan CreateInstance()
		{
			return (UI_ShuXingMianBan)UIPackage.CreateObject("UI_PetParticulars","ShuXingMianBan");
		}

		public UI_ShuXingMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_MianShangLvZhi = (GTextField)this.GetChildAt(4);
			m_ShangHaiLvZhi = (GTextField)this.GetChildAt(7);
			m_GeDangQiangDuZhi = (GTextField)this.GetChildAt(10);
			m_PoJiLvZhi = (GTextField)this.GetChildAt(13);
			m_GeDangLvZhi = (GTextField)this.GetChildAt(16);
			m_BaoJiQiangDuZhi = (GTextField)this.GetChildAt(19);
			m_KangBaoLvZhi = (GTextField)this.GetChildAt(22);
			m_BaoJiLvZhi = (GTextField)this.GetChildAt(25);
			m_fangyu = (GTextField)this.GetChildAt(28);
			m_ShengMingZhi = (GTextField)this.GetChildAt(31);
			m_GongJiZhi = (GTextField)this.GetChildAt(34);
		}
	}
}