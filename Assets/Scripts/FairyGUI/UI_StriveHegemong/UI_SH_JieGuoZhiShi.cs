/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_JieGuoZhiShi : GComponent
	{
		public GImage m_you;
		public GImage m_zuo;

		public const string URL = "ui://yjvxfmwogrok1j";

		public static UI_SH_JieGuoZhiShi CreateInstance()
		{
			return (UI_SH_JieGuoZhiShi)UIPackage.CreateObject("UI_StriveHegemong","SH_JieGuoZhiShi");
		}

		public UI_SH_JieGuoZhiShi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_you = (GImage)this.GetChildAt(0);
			m_zuo = (GImage)this.GetChildAt(1);
		}
	}
}