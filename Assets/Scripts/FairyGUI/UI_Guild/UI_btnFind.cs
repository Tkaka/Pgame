/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnFind : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64f1jg37";

		public static UI_btnFind CreateInstance()
		{
			return (UI_btnFind)UIPackage.CreateObject("UI_Guild","btnFind");
		}

		public UI_btnFind()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}