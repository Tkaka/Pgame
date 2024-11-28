/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_ConnectingWindow : GComponent
	{
		public GLoader m_fill;
		public GLoader m_icon;

		public const string URL = "ui://42sthz3tmvqvie";

		public static UI_ConnectingWindow CreateInstance()
		{
			return (UI_ConnectingWindow)UIPackage.CreateObject("UI_Common","ConnectingWindow");
		}

		public UI_ConnectingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_fill = (GLoader)this.GetChildAt(1);
			m_icon = (GLoader)this.GetChildAt(2);
		}
	}
}