/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ZS_OFF_ListIten : GComponent
	{
		public GTextField m_xianshi;

		public const string URL = "ui://yjvxfmwon7xzf";

		public static UI_SH_ZS_OFF_ListIten CreateInstance()
		{
			return (UI_SH_ZS_OFF_ListIten)UIPackage.CreateObject("UI_StriveHegemong","SH_ZS_OFF_ListIten");
		}

		public UI_SH_ZS_OFF_ListIten()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_xianshi = (GTextField)this.GetChildAt(1);
		}
	}
}