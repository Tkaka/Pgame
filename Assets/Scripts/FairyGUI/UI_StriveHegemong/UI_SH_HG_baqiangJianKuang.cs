/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_HG_baqiangJianKuang : GComponent
	{
		public UI_SH_HG_baqiangJianKuang_Item m_five;
		public UI_SH_HG_baqiangJianKuang_Item m_six;
		public UI_SH_HG_baqiangJianKuang_Item m_seven;
		public UI_SH_HG_Group m_Two;
		public UI_SH_HG_Group m_Three;
		public UI_SH_HG_Group m_Four;
		public UI_SH_HG_Group m_One;
		public UI_SH_JieGuoZhiShi m_zhishi_one;
		public UI_SH_JieGuoZhiShi m_zhishi_tow;
		public UI_SH_JieGuoZhiShi m_zhishi_three;
		public UI_SH_JieGuoZhiShi m_zhishi_four;
		public GGraph m_chakan_one;
		public GGraph m_chakan_five;
		public GGraph m_chakan_two;
		public GGraph m_chakan_seven;
		public GGraph m_chakan_three;
		public GGraph m_chakan_six;
		public GGraph m_chakan_four;
		public GImage m_zhishi_five;
		public GImage m_zhishi_six;
		public GGraph m_First;

		public const string URL = "ui://yjvxfmwon7xzq";

		public static UI_SH_HG_baqiangJianKuang CreateInstance()
		{
			return (UI_SH_HG_baqiangJianKuang)UIPackage.CreateObject("UI_StriveHegemong","SH_HG_baqiangJianKuang");
		}

		public UI_SH_HG_baqiangJianKuang()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_five = (UI_SH_HG_baqiangJianKuang_Item)this.GetChildAt(1);
			m_six = (UI_SH_HG_baqiangJianKuang_Item)this.GetChildAt(2);
			m_seven = (UI_SH_HG_baqiangJianKuang_Item)this.GetChildAt(3);
			m_Two = (UI_SH_HG_Group)this.GetChildAt(4);
			m_Three = (UI_SH_HG_Group)this.GetChildAt(5);
			m_Four = (UI_SH_HG_Group)this.GetChildAt(6);
			m_One = (UI_SH_HG_Group)this.GetChildAt(7);
			m_zhishi_one = (UI_SH_JieGuoZhiShi)this.GetChildAt(8);
			m_zhishi_tow = (UI_SH_JieGuoZhiShi)this.GetChildAt(9);
			m_zhishi_three = (UI_SH_JieGuoZhiShi)this.GetChildAt(10);
			m_zhishi_four = (UI_SH_JieGuoZhiShi)this.GetChildAt(11);
			m_chakan_one = (GGraph)this.GetChildAt(12);
			m_chakan_five = (GGraph)this.GetChildAt(13);
			m_chakan_two = (GGraph)this.GetChildAt(14);
			m_chakan_seven = (GGraph)this.GetChildAt(15);
			m_chakan_three = (GGraph)this.GetChildAt(16);
			m_chakan_six = (GGraph)this.GetChildAt(17);
			m_chakan_four = (GGraph)this.GetChildAt(18);
			m_zhishi_five = (GImage)this.GetChildAt(19);
			m_zhishi_six = (GImage)this.GetChildAt(20);
			m_First = (GGraph)this.GetChildAt(21);
		}
	}
}