/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_ItemComposeWindow : GComponent
	{
		public GGraph m_mask;
		public UI_itemComposePV m_popupView;

		public const string URL = "ui://g5pgln3nqaa2e4a";

		public static UI_ItemComposeWindow CreateInstance()
		{
			return (UI_ItemComposeWindow)UIPackage.CreateObject("UI_Beibao","ItemComposeWindow");
		}

		public UI_ItemComposeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_itemComposePV)this.GetChildAt(1);
		}
	}
}