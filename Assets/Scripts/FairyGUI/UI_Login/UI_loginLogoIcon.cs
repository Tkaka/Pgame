/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Login
{
	public partial class UI_loginLogoIcon : GComponent
	{
		public Transition m_anim;

		public const string URL = "ui://hg19ijpaeeh01o";

		public static UI_loginLogoIcon CreateInstance()
		{
			return (UI_loginLogoIcon)UIPackage.CreateObject("UI_Login","loginLogoIcon");
		}

		public UI_loginLogoIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_anim = this.GetTransitionAt(0);
		}
	}
}