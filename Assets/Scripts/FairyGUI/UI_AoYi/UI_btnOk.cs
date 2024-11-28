/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_btnOk : GComponent
	{
		public GImage m_title;

		public const string URL = "ui://vexa0xrynxtq1u";

		public static UI_btnOk CreateInstance()
		{
			return (UI_btnOk)UIPackage.CreateObject("UI_AoYi","btnOk");
		}

		public UI_btnOk()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title = (GImage)this.GetChildAt(1);
		}
	}
}