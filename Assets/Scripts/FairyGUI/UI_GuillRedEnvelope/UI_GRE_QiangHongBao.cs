/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_QiangHongBao : GComponent
	{
		public GList m_wodetongzhiList;
		public GList m_TaRenHongBaoList;

		public const string URL = "ui://r816m4tmfzr67";

		public static UI_GRE_QiangHongBao CreateInstance()
		{
			return (UI_GRE_QiangHongBao)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_QiangHongBao");
		}

		public UI_GRE_QiangHongBao()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_wodetongzhiList = (GList)this.GetChildAt(5);
			m_TaRenHongBaoList = (GList)this.GetChildAt(6);
		}
	}
}