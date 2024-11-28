/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_HongBaoBtn : GButton
	{
		public GImage m_hongdian;

		public const string URL = "ui://oe7ras64ovnab4c";

		public static UI_HongBaoBtn CreateInstance()
		{
			return (UI_HongBaoBtn)UIPackage.CreateObject("UI_Guild","HongBaoBtn");
		}

		public UI_HongBaoBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_hongdian = (GImage)this.GetChildAt(2);
		}
	}
}