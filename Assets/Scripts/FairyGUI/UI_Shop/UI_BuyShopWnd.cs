/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_BuyShopWnd : GComponent
	{
		public GGraph m_mask;
		public UI_buyShopPV m_popupView;

		public const string URL = "ui://w9mypx6jasm2g";

		public static UI_BuyShopWnd CreateInstance()
		{
			return (UI_BuyShopWnd)UIPackage.CreateObject("UI_Shop","BuyShopWnd");
		}

		public UI_BuyShopWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_buyShopPV)this.GetChildAt(1);
		}
	}
}