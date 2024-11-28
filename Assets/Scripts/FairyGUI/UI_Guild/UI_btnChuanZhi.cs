/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnChuanZhi : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92c";

		public static UI_btnChuanZhi CreateInstance()
		{
			return (UI_btnChuanZhi)UIPackage.CreateObject("UI_Guild","btnChuanZhi");
		}

		public UI_btnChuanZhi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}