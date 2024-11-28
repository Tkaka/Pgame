/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_mainCityChatCell : GComponent
	{
		public GRichTextField m_txtContent;
		public UI_objChannel m_objChannel;

		public const string URL = "ui://51gazvjdkb311y";

		public static UI_mainCityChatCell CreateInstance()
		{
			return (UI_mainCityChatCell)UIPackage.CreateObject("UI_Chat","mainCityChatCell");
		}

		public UI_mainCityChatCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtContent = (GRichTextField)this.GetChildAt(0);
			m_objChannel = (UI_objChannel)this.GetChildAt(1);
		}
	}
}