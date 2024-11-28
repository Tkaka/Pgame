/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_LogCell : GComponent
	{
		public GTextField m_txtDate;

		public const string URL = "ui://oe7ras64f1jg2q";

		public static UI_LogCell CreateInstance()
		{
			return (UI_LogCell)UIPackage.CreateObject("UI_Guild","LogCell");
		}

		public UI_LogCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDate = (GTextField)this.GetChildAt(1);
		}
	}
}