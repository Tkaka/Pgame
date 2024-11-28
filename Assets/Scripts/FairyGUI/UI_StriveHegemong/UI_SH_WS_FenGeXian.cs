/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_WS_FenGeXian : GComponent
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://yjvxfmwoje8y1x";

		public static UI_SH_WS_FenGeXian CreateInstance()
		{
			return (UI_SH_WS_FenGeXian)UIPackage.CreateObject("UI_StriveHegemong","SH_WS_FenGeXian");
		}

		public UI_SH_WS_FenGeXian()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}