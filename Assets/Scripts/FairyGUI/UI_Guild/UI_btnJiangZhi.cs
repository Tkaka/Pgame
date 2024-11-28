/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnJiangZhi : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92d";

		public static UI_btnJiangZhi CreateInstance()
		{
			return (UI_btnJiangZhi)UIPackage.CreateObject("UI_Guild","btnJiangZhi");
		}

		public UI_btnJiangZhi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}