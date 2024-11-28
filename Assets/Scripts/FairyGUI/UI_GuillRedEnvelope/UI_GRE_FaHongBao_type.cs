/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_FaHongBao_type : GComponent
	{
		public GTextField m_vip_level;
		public GLoader m_type_Icon;
		public GTextField m_name;
		public GLoader m_huode1_icon;
		public GLoader m_huode2_icon;
		public GTextField m_huode1_number;
		public GTextField m_huode2_number;
		public GLoader m_zonge_Icon;
		public GTextField m_zonge_Number;
		public GTextField m_jiage;
		public GLoader m_jiage_icon;
		public GButton m_FaHongBao;

		public const string URL = "ui://r816m4tmjh7wk";

		public static UI_GRE_FaHongBao_type CreateInstance()
		{
			return (UI_GRE_FaHongBao_type)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_FaHongBao_type");
		}

		public UI_GRE_FaHongBao_type()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_vip_level = (GTextField)this.GetChildAt(3);
			m_type_Icon = (GLoader)this.GetChildAt(6);
			m_name = (GTextField)this.GetChildAt(7);
			m_huode1_icon = (GLoader)this.GetChildAt(10);
			m_huode2_icon = (GLoader)this.GetChildAt(11);
			m_huode1_number = (GTextField)this.GetChildAt(12);
			m_huode2_number = (GTextField)this.GetChildAt(13);
			m_zonge_Icon = (GLoader)this.GetChildAt(14);
			m_zonge_Number = (GTextField)this.GetChildAt(15);
			m_jiage = (GTextField)this.GetChildAt(17);
			m_jiage_icon = (GLoader)this.GetChildAt(18);
			m_FaHongBao = (GButton)this.GetChildAt(19);
		}
	}
}