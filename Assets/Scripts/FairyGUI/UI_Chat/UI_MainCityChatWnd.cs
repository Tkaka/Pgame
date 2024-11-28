/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_MainCityChatWnd : GComponent
	{
		public GImage m_imgBg;
		public GList m_chatList;
		public GButton m_btnArrow;
		public GComponent m_btnSet;
		public GImage m_imgTrumpetBg;
		public UI_mainCityChatCell m_objInfo;
		public GGroup m_trumpetGroup;

		public const string URL = "ui://51gazvjdkb311x";

		public static UI_MainCityChatWnd CreateInstance()
		{
			return (UI_MainCityChatWnd)UIPackage.CreateObject("UI_Chat","MainCityChatWnd");
		}

		public UI_MainCityChatWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GImage)this.GetChildAt(0);
			m_chatList = (GList)this.GetChildAt(1);
			m_btnArrow = (GButton)this.GetChildAt(2);
			m_btnSet = (GComponent)this.GetChildAt(3);
			m_imgTrumpetBg = (GImage)this.GetChildAt(5);
			m_objInfo = (UI_mainCityChatCell)this.GetChildAt(6);
			m_trumpetGroup = (GGroup)this.GetChildAt(7);
		}
	}
}