/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_ChatWnd : GComponent
	{
		public Controller m_tabControl;
		public GGraph m_bg;
		public GList m_chatList;
		public UI_btnLaBa m_btnBell;
		public GButton m_btnLockScreen;
		public GComponent m_btnClose;
		public UI_btnYuYin m_btnVoice;
		public GTextInput m_txtInput;
		public UI_btnBiaoQing m_btnEmoji;
		public UI_btnSend m_btnSend;
		public UI_objNoReadInfo m_objNoRead;
		public GGroup m_chatGroup;

		public const string URL = "ui://51gazvjdfacb3";

		public static UI_ChatWnd CreateInstance()
		{
			return (UI_ChatWnd)UIPackage.CreateObject("UI_Chat","ChatWnd");
		}

		public UI_ChatWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tabControl = this.GetControllerAt(0);
			m_bg = (GGraph)this.GetChildAt(0);
			m_chatList = (GList)this.GetChildAt(9);
			m_btnBell = (UI_btnLaBa)this.GetChildAt(10);
			m_btnLockScreen = (GButton)this.GetChildAt(12);
			m_btnClose = (GComponent)this.GetChildAt(13);
			m_btnVoice = (UI_btnYuYin)this.GetChildAt(14);
			m_txtInput = (GTextInput)this.GetChildAt(16);
			m_btnEmoji = (UI_btnBiaoQing)this.GetChildAt(17);
			m_btnSend = (UI_btnSend)this.GetChildAt(18);
			m_objNoRead = (UI_objNoReadInfo)this.GetChildAt(23);
			m_chatGroup = (GGroup)this.GetChildAt(24);
		}
	}
}