/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnCancel : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92o";

		public static UI_btnCancel CreateInstance()
		{
			return (UI_btnCancel)UIPackage.CreateObject("UI_Guild","btnCancel");
		}

		public UI_btnCancel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}