/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_HG_MyCondition : GComponent
	{
		public GList m_WoDeZhanKuang;
		public GTextField m_zhankuang;
		public GTextField m_jifen;
		public GTextField m_paiming;
		public GTextField m_zhuti;

		public const string URL = "ui://yjvxfmwon7xzt";

		public static UI_SH_HG_MyCondition CreateInstance()
		{
			return (UI_SH_HG_MyCondition)UIPackage.CreateObject("UI_StriveHegemong","SH_HG_MyCondition");
		}

		public UI_SH_HG_MyCondition()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_WoDeZhanKuang = (GList)this.GetChildAt(4);
			m_zhankuang = (GTextField)this.GetChildAt(5);
			m_jifen = (GTextField)this.GetChildAt(7);
			m_paiming = (GTextField)this.GetChildAt(9);
			m_zhuti = (GTextField)this.GetChildAt(10);
		}
	}
}