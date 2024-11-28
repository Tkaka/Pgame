/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnCell : GButton
	{
		public GTextField m_txtDes;
		public GImage m_imgRed;

		public const string URL = "ui://oe7ras64qbwui";

		public static UI_btnCell CreateInstance()
		{
			return (UI_btnCell)UIPackage.CreateObject("UI_Guild","btnCell");
		}

		public UI_btnCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
			m_imgRed = (GImage)this.GetChildAt(3);
		}
	}
}