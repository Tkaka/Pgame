/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnOk : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde926";

		public static UI_btnOk CreateInstance()
		{
			return (UI_btnOk)UIPackage.CreateObject("UI_Guild","btnOk");
		}

		public UI_btnOk()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}