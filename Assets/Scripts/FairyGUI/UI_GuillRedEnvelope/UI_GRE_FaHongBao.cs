/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_FaHongBao : GComponent
	{
		public GList m_honhbaoList;
		public GTextField m_number;

		public const string URL = "ui://r816m4tmfzr6c";

		public static UI_GRE_FaHongBao CreateInstance()
		{
			return (UI_GRE_FaHongBao)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_FaHongBao");
		}

		public UI_GRE_FaHongBao()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_honhbaoList = (GList)this.GetChildAt(0);
			m_number = (GTextField)this.GetChildAt(2);
		}
	}
}