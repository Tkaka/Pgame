/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_GZ_Jiangli : GComponent
	{
		public GTextField m_fanwei;
		public GList m_jingpin;

		public const string URL = "ui://yjvxfmwok46r22";

		public static UI_SH_GZ_Jiangli CreateInstance()
		{
			return (UI_SH_GZ_Jiangli)UIPackage.CreateObject("UI_StriveHegemong","SH_GZ_Jiangli");
		}

		public UI_SH_GZ_Jiangli()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_fanwei = (GTextField)this.GetChildAt(1);
			m_jingpin = (GList)this.GetChildAt(2);
		}
	}
}