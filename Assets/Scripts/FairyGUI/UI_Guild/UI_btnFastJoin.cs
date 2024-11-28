/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnFastJoin : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64f1jg31";

		public static UI_btnFastJoin CreateInstance()
		{
			return (UI_btnFastJoin)UIPackage.CreateObject("UI_Guild","btnFastJoin");
		}

		public UI_btnFastJoin()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}