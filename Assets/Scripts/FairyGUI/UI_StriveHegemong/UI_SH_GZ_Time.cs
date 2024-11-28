/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_GZ_Time : GComponent
	{
		public GTextField m_OneTime;
		public GTextField m_TwoTime;
		public GTextField m_ThreeTime;
		public GTextField m_OneAffair;
		public GTextField m_TwoAffair;
		public GTextField m_ThreeAffair;

		public const string URL = "ui://yjvxfmwok46r23";

		public static UI_SH_GZ_Time CreateInstance()
		{
			return (UI_SH_GZ_Time)UIPackage.CreateObject("UI_StriveHegemong","SH_GZ_Time");
		}

		public UI_SH_GZ_Time()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_OneTime = (GTextField)this.GetChildAt(4);
			m_TwoTime = (GTextField)this.GetChildAt(5);
			m_ThreeTime = (GTextField)this.GetChildAt(6);
			m_OneAffair = (GTextField)this.GetChildAt(7);
			m_TwoAffair = (GTextField)this.GetChildAt(8);
			m_ThreeAffair = (GTextField)this.GetChildAt(9);
		}
	}
}