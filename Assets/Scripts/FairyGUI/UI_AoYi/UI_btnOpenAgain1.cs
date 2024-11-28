/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_btnOpenAgain1 : GComponent
	{
		public GImage m_title;

		public const string URL = "ui://vexa0xrynxtq1s";

		public static UI_btnOpenAgain1 CreateInstance()
		{
			return (UI_btnOpenAgain1)UIPackage.CreateObject("UI_AoYi","btnOpenAgain1");
		}

		public UI_btnOpenAgain1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title = (GImage)this.GetChildAt(1);
		}
	}
}