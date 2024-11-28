/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_TheHighestItem : GComponent
	{
		public GTextField m_allmoney;
		public GTextField m_number;
		public GTextField m_zuigao;

		public const string URL = "ui://r816m4tmfzr6f";

		public static UI_GRE_TheHighestItem CreateInstance()
		{
			return (UI_GRE_TheHighestItem)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_TheHighestItem");
		}

		public UI_GRE_TheHighestItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_allmoney = (GTextField)this.GetChildAt(1);
			m_number = (GTextField)this.GetChildAt(2);
			m_zuigao = (GTextField)this.GetChildAt(3);
		}
	}
}