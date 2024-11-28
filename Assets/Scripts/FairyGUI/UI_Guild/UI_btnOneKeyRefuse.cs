/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnOneKeyRefuse : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://oe7ras64fde92k";

		public static UI_btnOneKeyRefuse CreateInstance()
		{
			return (UI_btnOneKeyRefuse)UIPackage.CreateObject("UI_Guild","btnOneKeyRefuse");
		}

		public UI_btnOneKeyRefuse()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}