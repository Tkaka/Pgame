/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_otherChatCell : GComponent
	{
		public UI_objTouXiang m_imgIcon;
		public UI_btnJump m_btnJump;
		public GImage m_imgBubble;
		public GRichTextField m_txtContent;
		public GTextField m_name;
		public UI_objVip m_vip;
		public UI_objChannel m_channel;

		public const string URL = "ui://51gazvjd7igt1b";

		public static UI_otherChatCell CreateInstance()
		{
			return (UI_otherChatCell)UIPackage.CreateObject("UI_Chat","otherChatCell");
		}

		public UI_otherChatCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcon = (UI_objTouXiang)this.GetChildAt(0);
			m_btnJump = (UI_btnJump)this.GetChildAt(1);
			m_imgBubble = (GImage)this.GetChildAt(2);
			m_txtContent = (GRichTextField)this.GetChildAt(3);
			m_name = (GTextField)this.GetChildAt(5);
			m_vip = (UI_objVip)this.GetChildAt(6);
			m_channel = (UI_objChannel)this.GetChildAt(7);
		}
	}
}