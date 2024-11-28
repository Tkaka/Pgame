/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SiginIn
{
	public partial class UI_SiginInWindow : GComponent
	{
		public GGraph m_mask;
		public UI_siginInPV m_popupView;

		public const string URL = "ui://jbviry4zdaur0";

		public static UI_SiginInWindow CreateInstance()
		{
			return (UI_SiginInWindow)UIPackage.CreateObject("UI_SiginIn","SiginInWindow");
		}

		public UI_SiginInWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_siginInPV)this.GetChildAt(1);
		}
	}
}