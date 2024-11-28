/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_SuiPianBuyShopWnd : GComponent
	{
		public GGraph m_mask;
		public UI_suiPianBuyPV m_popupView;

		public const string URL = "ui://w9mypx6jiy9sw";

		public static UI_SuiPianBuyShopWnd CreateInstance()
		{
			return (UI_SuiPianBuyShopWnd)UIPackage.CreateObject("UI_Shop","SuiPianBuyShopWnd");
		}

		public UI_SuiPianBuyShopWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_suiPianBuyPV)this.GetChildAt(1);
		}
	}
}