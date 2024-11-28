/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_GuanKaListBtn : GButton
	{
		public GTextField m_CiShu;
		public GGroup m_YiKaiQi;
		public GTextField m_weikaiqi;
		public GGroup m_WeiKaiQi;
		public GGroup m_QianWang;

		public const string URL = "ui://4asqm7awiou162";

		public static UI_GuanKaListBtn CreateInstance()
		{
			return (UI_GuanKaListBtn)UIPackage.CreateObject("UI_GeDouJia","GuanKaListBtn");
		}

		public UI_GuanKaListBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_CiShu = (GTextField)this.GetChildAt(1);
			m_YiKaiQi = (GGroup)this.GetChildAt(2);
			m_weikaiqi = (GTextField)this.GetChildAt(4);
			m_WeiKaiQi = (GGroup)this.GetChildAt(5);
			m_QianWang = (GGroup)this.GetChildAt(8);
		}
	}
}