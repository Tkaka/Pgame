/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_SelfChatCell : GComponent
	{
		public UI_objTouXiang m_imgIcon;
		public GImage m_imgBubble;
		public GRichTextField m_txtContent;
		public GTextField m_name;
		public UI_objVip m_vip;
		public UI_objChannel m_channel;

		public const string URL = "ui://51gazvjdl0n11j";

		public static UI_SelfChatCell CreateInstance()
		{
			return (UI_SelfChatCell)UIPackage.CreateObject("UI_Chat","SelfChatCell");
		}

		public UI_SelfChatCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcon = (UI_objTouXiang)this.GetChildAt(0);
			m_imgBubble = (GImage)this.GetChildAt(1);
			m_txtContent = (GRichTextField)this.GetChildAt(2);
			m_name = (GTextField)this.GetChildAt(4);
			m_vip = (UI_objVip)this.GetChildAt(5);
			m_channel = (UI_objChannel)this.GetChildAt(6);
		}
	}
}