/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_HangPointnInfo : GComponent
	{
		public GTextField m_txtInfo;

		public const string URL = "ui://oe7ras64f1jg3c";

		public static UI_HangPointnInfo CreateInstance()
		{
			return (UI_HangPointnInfo)UIPackage.CreateObject("UI_Guild","HangPointnInfo");
		}

		public UI_HangPointnInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtInfo = (GTextField)this.GetChildAt(0);
		}
	}
}