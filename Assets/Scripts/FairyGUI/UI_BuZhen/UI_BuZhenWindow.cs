/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_BuZhenWindow : GComponent
	{
		public GGraph m_bg;
		public GComponent m_commonTop;
		public GGraph m_grid00;
		public GGraph m_grid01;
		public GGraph m_grid02;
		public GGraph m_grid10;
		public GGraph m_grid11;
		public GGraph m_grid12;
		public GGraph m_grid20;
		public GGraph m_grid21;
		public GGraph m_grid22;
		public GGraph m_grid30;
		public GGraph m_grid31;
		public GGraph m_grid32;
		public GImage m_hightLight;
		public UI_BuZhenHolder m_pos1;
		public UI_BuZhenHolder m_pos4;
		public UI_BuZhenHolder m_pos2;
		public UI_BuZhenHolder m_pos5;
		public UI_BuZhenHolder m_pos0;
		public UI_BuZhenHolder m_pos3;
		public GTextField m_shangZhenNum;
		public GList m_backPropertyList;
		public GList m_frontPropertyList;
		public GButton m_starFightBtn;

		public const string URL = "ui://z0csav43peysn";

		public static UI_BuZhenWindow CreateInstance()
		{
			return (UI_BuZhenWindow)UIPackage.CreateObject("UI_BuZhen","BuZhenWindow");
		}

		public UI_BuZhenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GGraph)this.GetChildAt(1);
			m_commonTop = (GComponent)this.GetChildAt(3);
			m_grid00 = (GGraph)this.GetChildAt(10);
			m_grid01 = (GGraph)this.GetChildAt(11);
			m_grid02 = (GGraph)this.GetChildAt(12);
			m_grid10 = (GGraph)this.GetChildAt(13);
			m_grid11 = (GGraph)this.GetChildAt(14);
			m_grid12 = (GGraph)this.GetChildAt(15);
			m_grid20 = (GGraph)this.GetChildAt(16);
			m_grid21 = (GGraph)this.GetChildAt(17);
			m_grid22 = (GGraph)this.GetChildAt(18);
			m_grid30 = (GGraph)this.GetChildAt(19);
			m_grid31 = (GGraph)this.GetChildAt(20);
			m_grid32 = (GGraph)this.GetChildAt(21);
			m_hightLight = (GImage)this.GetChildAt(22);
			m_pos1 = (UI_BuZhenHolder)this.GetChildAt(23);
			m_pos4 = (UI_BuZhenHolder)this.GetChildAt(24);
			m_pos2 = (UI_BuZhenHolder)this.GetChildAt(25);
			m_pos5 = (UI_BuZhenHolder)this.GetChildAt(26);
			m_pos0 = (UI_BuZhenHolder)this.GetChildAt(27);
			m_pos3 = (UI_BuZhenHolder)this.GetChildAt(28);
			m_shangZhenNum = (GTextField)this.GetChildAt(30);
			m_backPropertyList = (GList)this.GetChildAt(37);
			m_frontPropertyList = (GList)this.GetChildAt(38);
			m_starFightBtn = (GButton)this.GetChildAt(40);
		}
	}
}