/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_normalChatCell : GComponent
	{
		public GRichTextField m_txtContent;
		public UI_objChannel m_channel;

		public const string URL = "ui://51gazvjdkb311k";

		public static UI_normalChatCell CreateInstance()
		{
			return (UI_normalChatCell)UIPackage.CreateObject("UI_Chat","normalChatCell");
		}

		public UI_normalChatCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtContent = (GRichTextField)this.GetChildAt(0);
			m_channel = (UI_objChannel)this.GetChildAt(1);
		}
	}
}