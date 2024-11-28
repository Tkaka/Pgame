/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_GZ_FenGeXian : GComponent
	{
		public GTextField m_name;

		public const string URL = "ui://yjvxfmwoidnd19";

		public static UI_SH_GZ_FenGeXian CreateInstance()
		{
			return (UI_SH_GZ_FenGeXian)UIPackage.CreateObject("UI_StriveHegemong","SH_GZ_FenGeXian");
		}

		public UI_SH_GZ_FenGeXian()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_name = (GTextField)this.GetChildAt(1);
		}
	}
}