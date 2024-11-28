/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GER_PaiHangWindow : GComponent
	{
		public GTextField m_name;
		public GButton m_closeBtn;
		public GList m_ranklist;
		public GTextField m_geshu;
		public GTextField m_zongliang;
		public GGroup m_FaHongBao;
		public GTextField m_de_number;
		public GGroup m_QiangHongBao;

		public const string URL = "ui://r816m4tmfzr6b";

		public static UI_GER_PaiHangWindow CreateInstance()
		{
			return (UI_GER_PaiHangWindow)UIPackage.CreateObject("UI_GuillRedEnvelope","GER_PaiHangWindow");
		}

		public UI_GER_PaiHangWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_name = (GTextField)this.GetChildAt(6);
			m_closeBtn = (GButton)this.GetChildAt(7);
			m_ranklist = (GList)this.GetChildAt(8);
			m_geshu = (GTextField)this.GetChildAt(11);
			m_zongliang = (GTextField)this.GetChildAt(12);
			m_FaHongBao = (GGroup)this.GetChildAt(15);
			m_de_number = (GTextField)this.GetChildAt(16);
			m_QiangHongBao = (GGroup)this.GetChildAt(18);
		}
	}
}