/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_badgeCell : GComponent
	{
		public GLoader m_imgIcon;

		public const string URL = "ui://oe7ras64fde924";

		public static UI_badgeCell CreateInstance()
		{
			return (UI_badgeCell)UIPackage.CreateObject("UI_Guild","badgeCell");
		}

		public UI_badgeCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcon = (GLoader)this.GetChildAt(1);
		}
	}
}