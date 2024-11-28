/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_laiYuanPV : GComponent
	{
		public GTextField m_Name;
		public GTextField m_number;
		public GLoader m_PinJie;
		public GLoader m_TouXiang;
		public GImage m_type;
		public GButton m_Close;
		public GList m_GuanKaList;
		public GComboBox m_CiShuXuanZe;
		public GTextField m_JingQingQiDai;

		public const string URL = "ui://4asqm7awlxvz6a";

		public static UI_laiYuanPV CreateInstance()
		{
			return (UI_laiYuanPV)UIPackage.CreateObject("UI_GeDouJia","laiYuanPV");
		}

		public UI_laiYuanPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Name = (GTextField)this.GetChildAt(4);
			m_number = (GTextField)this.GetChildAt(6);
			m_PinJie = (GLoader)this.GetChildAt(7);
			m_TouXiang = (GLoader)this.GetChildAt(8);
			m_type = (GImage)this.GetChildAt(9);
			m_Close = (GButton)this.GetChildAt(10);
			m_GuanKaList = (GList)this.GetChildAt(11);
			m_CiShuXuanZe = (GComboBox)this.GetChildAt(14);
			m_JingQingQiDai = (GTextField)this.GetChildAt(15);
		}
	}
}