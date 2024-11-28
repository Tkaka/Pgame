/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_btnOpenAgain10 : GComponent
	{
		public GImage m_title;

		public const string URL = "ui://vexa0xrynxtq1t";

		public static UI_btnOpenAgain10 CreateInstance()
		{
			return (UI_btnOpenAgain10)UIPackage.CreateObject("UI_AoYi","btnOpenAgain10");
		}

		public UI_btnOpenAgain10()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title = (GImage)this.GetChildAt(1);
		}
	}
}