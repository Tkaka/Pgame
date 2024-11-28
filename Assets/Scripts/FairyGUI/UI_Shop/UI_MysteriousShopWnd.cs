/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_MysteriousShopWnd : GComponent
	{
		public GGraph m_mask;
		public UI_MysteriousShopPV m_popupView;

		public const string URL = "ui://w9mypx6jhsph1b";

		public static UI_MysteriousShopWnd CreateInstance()
		{
			return (UI_MysteriousShopWnd)UIPackage.CreateObject("UI_Shop","MysteriousShopWnd");
		}

		public UI_MysteriousShopWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_MysteriousShopPV)this.GetChildAt(1);
		}
	}
}