/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_ItemSellWindow : GComponent
	{
		public GGraph m_mask;
		public UI_itemSellPV m_popupView;

		public const string URL = "ui://g5pgln3ngepc1m";

		public static UI_ItemSellWindow CreateInstance()
		{
			return (UI_ItemSellWindow)UIPackage.CreateObject("UI_Beibao","ItemSellWindow");
		}

		public UI_ItemSellWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_itemSellPV)this.GetChildAt(1);
		}
	}
}