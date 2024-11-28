/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ZS_OFF : GComponent
	{
		public GTextField m_moshi;
		public GTextField m_Changci;
		public GList m_xianshiList;
		public GTextField m_time;

		public const string URL = "ui://yjvxfmwon7xze";

		public static UI_SH_ZS_OFF CreateInstance()
		{
			return (UI_SH_ZS_OFF)UIPackage.CreateObject("UI_StriveHegemong","SH_ZS_OFF");
		}

		public UI_SH_ZS_OFF()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_moshi = (GTextField)this.GetChildAt(2);
			m_Changci = (GTextField)this.GetChildAt(3);
			m_xianshiList = (GList)this.GetChildAt(5);
			m_time = (GTextField)this.GetChildAt(8);
		}
	}
}