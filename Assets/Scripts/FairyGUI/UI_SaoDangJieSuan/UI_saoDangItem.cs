/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_saoDangItem : GComponent
	{
		public GLoader m_PingJieKuang;
		public GLoader m_TouXiangKuang;

		public const string URL = "ui://34cd5b4hwmio2i";

		public static UI_saoDangItem CreateInstance()
		{
			return (UI_saoDangItem)UIPackage.CreateObject("UI_SaoDangJieSuan","saoDangItem");
		}

		public UI_saoDangItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_PingJieKuang = (GLoader)this.GetChildAt(0);
			m_TouXiangKuang = (GLoader)this.GetChildAt(1);
		}
	}
}