/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_Top_Qiang_ListItem : GComponent
	{
		public GLoader m_type_icon;
		public GTextField m_name;
		public GTextField m_number;
		public UI_GRE_QiangHongBtn m_qianghongbaoBtn;
		public GTextField m_yiqiangwan;

		public const string URL = "ui://r816m4tmjh7wl";

		public static UI_GRE_Top_Qiang_ListItem CreateInstance()
		{
			return (UI_GRE_Top_Qiang_ListItem)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_Top_Qiang_ListItem");
		}

		public UI_GRE_Top_Qiang_ListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_type_icon = (GLoader)this.GetChildAt(1);
			m_name = (GTextField)this.GetChildAt(2);
			m_number = (GTextField)this.GetChildAt(3);
			m_qianghongbaoBtn = (UI_GRE_QiangHongBtn)this.GetChildAt(4);
			m_yiqiangwan = (GTextField)this.GetChildAt(5);
		}
	}
}