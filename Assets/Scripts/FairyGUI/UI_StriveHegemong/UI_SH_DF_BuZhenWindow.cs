/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_DF_BuZhenWindow : GComponent
	{
		public GImage m_BeiJing;
		public GList m_BuZhenList;
		public GButton m_BaoCunBtn;
		public GButton m_TuiChuBtn;
		public GGraph m_pos1;
		public GGraph m_pos2;
		public GGraph m_pos3;
		public GGraph m_pos4;
		public GGraph m_pos5;
		public GGraph m_pos6;
		public GGraph m_pos7;
		public GGraph m_pos8;
		public GGraph m_pos9;
		public GGraph m_pos10;

		public const string URL = "ui://yjvxfmwoje8y1y";

		public static UI_SH_DF_BuZhenWindow CreateInstance()
		{
			return (UI_SH_DF_BuZhenWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_DF_BuZhenWindow");
		}

		public UI_SH_DF_BuZhenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GImage)this.GetChildAt(0);
			m_BuZhenList = (GList)this.GetChildAt(3);
			m_BaoCunBtn = (GButton)this.GetChildAt(4);
			m_TuiChuBtn = (GButton)this.GetChildAt(5);
			m_pos1 = (GGraph)this.GetChildAt(6);
			m_pos2 = (GGraph)this.GetChildAt(7);
			m_pos3 = (GGraph)this.GetChildAt(8);
			m_pos4 = (GGraph)this.GetChildAt(9);
			m_pos5 = (GGraph)this.GetChildAt(10);
			m_pos6 = (GGraph)this.GetChildAt(11);
			m_pos7 = (GGraph)this.GetChildAt(12);
			m_pos8 = (GGraph)this.GetChildAt(13);
			m_pos9 = (GGraph)this.GetChildAt(14);
			m_pos10 = (GGraph)this.GetChildAt(15);
		}
	}
}