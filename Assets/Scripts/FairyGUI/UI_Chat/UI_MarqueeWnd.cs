/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_MarqueeWnd : GComponent
	{
		public GImage m_imgBg;
		public GRichTextField m_txtDes;

		public const string URL = "ui://51gazvjd10ifg26";

		public static UI_MarqueeWnd CreateInstance()
		{
			return (UI_MarqueeWnd)UIPackage.CreateObject("UI_Chat","MarqueeWnd");
		}

		public UI_MarqueeWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GImage)this.GetChildAt(0);
			m_txtDes = (GRichTextField)this.GetChildAt(1);
		}
	}
}