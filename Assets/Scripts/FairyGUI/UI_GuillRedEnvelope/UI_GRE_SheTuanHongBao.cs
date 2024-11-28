/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_SheTuanHongBao : GComponent
	{
		public GList m_shetuanhongbaoList;
		public GList m_topOneList;

		public const string URL = "ui://r816m4tmfzr69";

		public static UI_GRE_SheTuanHongBao CreateInstance()
		{
			return (UI_GRE_SheTuanHongBao)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_SheTuanHongBao");
		}

		public UI_GRE_SheTuanHongBao()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_shetuanhongbaoList = (GList)this.GetChildAt(0);
			m_topOneList = (GList)this.GetChildAt(1);
		}
	}
}