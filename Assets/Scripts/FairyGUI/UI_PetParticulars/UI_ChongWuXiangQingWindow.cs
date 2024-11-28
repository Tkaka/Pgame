/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_ChongWuXiangQingWindow : GComponent
	{
		public GTextField m_ZiZhi;
		public GList m_XiangQingList;
		public GButton m_lastBtn;
		public GButton m_nextBtn;
		public GGraph m_xianshiqiehuan;
		public GGraph m_Model;
		public GLoader m_typeLoader;
		public GTextField m_Name;
		public GButton m_JinHuaLianBtn;
		public GList m_starList;
		public UI_ShuXingMianBan m_ShuXingMianBan;
		public UI_nextBtn m_next;
		public UI_lastBtn m_last;
		public GGraph m_colseBtn;
		public GGroup m_FanHui;
		public Transition m_zhaunhuan;

		public const string URL = "ui://rn5o3g4tfzr60";

		public static UI_ChongWuXiangQingWindow CreateInstance()
		{
			return (UI_ChongWuXiangQingWindow)UIPackage.CreateObject("UI_PetParticulars","ChongWuXiangQingWindow");
		}

		public UI_ChongWuXiangQingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ZiZhi = (GTextField)this.GetChildAt(2);
			m_XiangQingList = (GList)this.GetChildAt(3);
			m_lastBtn = (GButton)this.GetChildAt(4);
			m_nextBtn = (GButton)this.GetChildAt(5);
			m_xianshiqiehuan = (GGraph)this.GetChildAt(6);
			m_Model = (GGraph)this.GetChildAt(8);
			m_typeLoader = (GLoader)this.GetChildAt(10);
			m_Name = (GTextField)this.GetChildAt(11);
			m_JinHuaLianBtn = (GButton)this.GetChildAt(12);
			m_starList = (GList)this.GetChildAt(13);
			m_ShuXingMianBan = (UI_ShuXingMianBan)this.GetChildAt(15);
			m_next = (UI_nextBtn)this.GetChildAt(16);
			m_last = (UI_lastBtn)this.GetChildAt(17);
			m_colseBtn = (GGraph)this.GetChildAt(20);
			m_FanHui = (GGroup)this.GetChildAt(21);
			m_zhaunhuan = this.GetTransitionAt(0);
		}
	}
}