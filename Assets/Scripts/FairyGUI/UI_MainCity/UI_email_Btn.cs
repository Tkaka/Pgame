/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_email_Btn : GButton
	{
		public GImage m_imgRed;
		public Transition m_anim;

		public const string URL = "ui://jdfufi06ro1f65";

		public static UI_email_Btn CreateInstance()
		{
			return (UI_email_Btn)UIPackage.CreateObject("UI_MainCity","email_Btn");
		}

		public UI_email_Btn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgRed = (GImage)this.GetChildAt(1);
			m_anim = this.GetTransitionAt(0);
		}
	}
}