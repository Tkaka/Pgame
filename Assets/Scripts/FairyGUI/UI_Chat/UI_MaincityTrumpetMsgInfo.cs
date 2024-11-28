/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_MaincityTrumpetMsgInfo : GComponent
	{
		public GImage m_imgBg;
		public GTextField m_txtMsg;
		public GGroup m_msgGroup;

		public const string URL = "ui://51gazvjdkb311w";

		public static UI_MaincityTrumpetMsgInfo CreateInstance()
		{
			return (UI_MaincityTrumpetMsgInfo)UIPackage.CreateObject("UI_Chat","MaincityTrumpetMsgInfo");
		}

		public UI_MaincityTrumpetMsgInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GImage)this.GetChildAt(0);
			m_txtMsg = (GTextField)this.GetChildAt(1);
			m_msgGroup = (GGroup)this.GetChildAt(2);
		}
	}
}