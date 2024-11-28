/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_ChatEmojiWnd : GComponent
	{
		public GGraph m_imgBg;
		public GComponent m_btnClose;
		public GList m_emojiList;
		public GGroup m_wndGroup;

		public const string URL = "ui://51gazvjd10ifg27";

		public static UI_ChatEmojiWnd CreateInstance()
		{
			return (UI_ChatEmojiWnd)UIPackage.CreateObject("UI_Chat","ChatEmojiWnd");
		}

		public UI_ChatEmojiWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GGraph)this.GetChildAt(0);
			m_btnClose = (GComponent)this.GetChildAt(3);
			m_emojiList = (GList)this.GetChildAt(4);
			m_wndGroup = (GGroup)this.GetChildAt(5);
		}
	}
}