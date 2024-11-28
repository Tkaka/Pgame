/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnColor : GComponent
	{
		public GImage m_imgColor;
		public GTextField m_txtColor;

		public const string URL = "ui://51gazvjdkb311u";

		public static UI_btnColor CreateInstance()
		{
			return (UI_btnColor)UIPackage.CreateObject("UI_Chat","btnColor");
		}

		public UI_btnColor()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgColor = (GImage)this.GetChildAt(0);
			m_txtColor = (GTextField)this.GetChildAt(1);
		}
	}
}