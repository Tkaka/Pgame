/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_FaHongBaoListItem : GComponent
	{
		public GTextField m_GRE_PaiMingText;
		public GLoader m_PaiMingIcon;
		public GTextField m_name;
		public GTextField m_number;

		public const string URL = "ui://r816m4tmfzr6g";

		public static UI_GRE_FaHongBaoListItem CreateInstance()
		{
			return (UI_GRE_FaHongBaoListItem)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_FaHongBaoListItem");
		}

		public UI_GRE_FaHongBaoListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GRE_PaiMingText = (GTextField)this.GetChildAt(2);
			m_PaiMingIcon = (GLoader)this.GetChildAt(3);
			m_name = (GTextField)this.GetChildAt(4);
			m_number = (GTextField)this.GetChildAt(5);
		}
	}
}