/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_ItemUseWindow : GComponent
	{
		public GGraph m_mask;
		public UI_itemUsePV m_popupView;

		public const string URL = "ui://g5pgln3nc1yd1w";

		public static UI_ItemUseWindow CreateInstance()
		{
			return (UI_ItemUseWindow)UIPackage.CreateObject("UI_Beibao","ItemUseWindow");
		}

		public UI_ItemUseWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_itemUsePV)this.GetChildAt(1);
		}
	}
}