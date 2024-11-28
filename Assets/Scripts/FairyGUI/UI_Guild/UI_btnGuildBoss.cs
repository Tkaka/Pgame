/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnGuildBoss : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64qksdb4b";

		public static UI_btnGuildBoss CreateInstance()
		{
			return (UI_btnGuildBoss)UIPackage.CreateObject("UI_Guild","btnGuildBoss");
		}

		public UI_btnGuildBoss()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}