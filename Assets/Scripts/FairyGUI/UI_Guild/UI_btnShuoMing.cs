/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnShuoMing : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64qbwu1r";

		public static UI_btnShuoMing CreateInstance()
		{
			return (UI_btnShuoMing)UIPackage.CreateObject("UI_Guild","btnShuoMing");
		}

		public UI_btnShuoMing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}