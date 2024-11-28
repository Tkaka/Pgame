/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_BaoMingBtn : GButton
	{
		public GTextField m_baoming;
		public GTextField m_yibaoming;

		public const string URL = "ui://yjvxfmwon7xzc";

		public static UI_SH_BaoMingBtn CreateInstance()
		{
			return (UI_SH_BaoMingBtn)UIPackage.CreateObject("UI_StriveHegemong","SH_BaoMingBtn");
		}

		public UI_SH_BaoMingBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_baoming = (GTextField)this.GetChildAt(1);
			m_yibaoming = (GTextField)this.GetChildAt(2);
		}
	}
}