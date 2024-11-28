/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_ZhanTingWindow : GComponent
	{
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;
		public GButton m_backBtn;
		public GTextField m_jiaChengLabel;
		public GTextField m_totalValueLabel;
		public GTextField m_atkLabel;
		public GTextField m_defLabel;
		public GTextField m_hpLabel;
		public GLoader m_typeLoader;
		public GGraph m_touchHolder;
		public GGraph m_secondTXPos;
		public GGraph m_firstTXPos;
		public GGraph m_fifthTXPos;
		public GGraph m_fourthTXPos;
		public GGraph m_thridTXPos;

		public const string URL = "ui://ansp6fm5lni71";

		public static UI_ZhanTingWindow CreateInstance()
		{
			return (UI_ZhanTingWindow)UIPackage.CreateObject("UI_TongXiangGuan","ZhanTingWindow");
		}

		public UI_ZhanTingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_switchLeftBtn = (GButton)this.GetChildAt(1);
			m_switchRightBtn = (GButton)this.GetChildAt(2);
			m_backBtn = (GButton)this.GetChildAt(3);
			m_jiaChengLabel = (GTextField)this.GetChildAt(6);
			m_totalValueLabel = (GTextField)this.GetChildAt(10);
			m_atkLabel = (GTextField)this.GetChildAt(14);
			m_defLabel = (GTextField)this.GetChildAt(15);
			m_hpLabel = (GTextField)this.GetChildAt(16);
			m_typeLoader = (GLoader)this.GetChildAt(17);
			m_touchHolder = (GGraph)this.GetChildAt(19);
			m_secondTXPos = (GGraph)this.GetChildAt(20);
			m_firstTXPos = (GGraph)this.GetChildAt(21);
			m_fifthTXPos = (GGraph)this.GetChildAt(22);
			m_fourthTXPos = (GGraph)this.GetChildAt(23);
			m_thridTXPos = (GGraph)this.GetChildAt(24);
		}
	}
}