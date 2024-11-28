/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_TongXiangShuXingWindow : GComponent
	{
		public GGraph m_modelPos;
		public GTextField m_rankLabel;
		public GTextField m_nameLabel;
		public GTextField m_colorLabel;
		public GLoader m_typeLoader;
		public GTextField m_jiaChengLabel;
		public GTextField m_atkLabel;
		public GTextField m_defLabel;
		public GTextField m_hpLabel;
		public GTextField m_colorAddLabel;
		public GTextField m_tongXiangAddLabel;
		public GTextField m_valueLabel;
		public GButton m_switchBtn;
		public GButton m_closeBtn;
		public GRichTextField m_starAddLabel;

		public const string URL = "ui://ansp6fm5lni72";

		public static UI_TongXiangShuXingWindow CreateInstance()
		{
			return (UI_TongXiangShuXingWindow)UIPackage.CreateObject("UI_TongXiangGuan","TongXiangShuXingWindow");
		}

		public UI_TongXiangShuXingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_modelPos = (GGraph)this.GetChildAt(2);
			m_rankLabel = (GTextField)this.GetChildAt(3);
			m_nameLabel = (GTextField)this.GetChildAt(4);
			m_colorLabel = (GTextField)this.GetChildAt(5);
			m_typeLoader = (GLoader)this.GetChildAt(6);
			m_jiaChengLabel = (GTextField)this.GetChildAt(7);
			m_atkLabel = (GTextField)this.GetChildAt(11);
			m_defLabel = (GTextField)this.GetChildAt(12);
			m_hpLabel = (GTextField)this.GetChildAt(13);
			m_colorAddLabel = (GTextField)this.GetChildAt(19);
			m_tongXiangAddLabel = (GTextField)this.GetChildAt(20);
			m_valueLabel = (GTextField)this.GetChildAt(23);
			m_switchBtn = (GButton)this.GetChildAt(24);
			m_closeBtn = (GButton)this.GetChildAt(25);
			m_starAddLabel = (GRichTextField)this.GetChildAt(26);
		}
	}
}