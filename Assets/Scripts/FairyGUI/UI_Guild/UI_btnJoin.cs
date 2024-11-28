/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnJoin : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64f1jg2x";

		public static UI_btnJoin CreateInstance()
		{
			return (UI_btnJoin)UIPackage.CreateObject("UI_Guild","btnJoin");
		}

		public UI_btnJoin()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}