/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_sendMail : GComponent
	{
		public GTextField m_txtMailNum;

		public const string URL = "ui://oe7ras64qbwu1q";

		public static UI_sendMail CreateInstance()
		{
			return (UI_sendMail)UIPackage.CreateObject("UI_Guild","sendMail");
		}

		public UI_sendMail()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtMailNum = (GTextField)this.GetChildAt(1);
		}
	}
}