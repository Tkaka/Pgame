/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_SuiPianDuiHuanWnd : GComponent
	{
		public GGraph m_mask;
		public UI_suiPianExchangePV m_popupView;

		public const string URL = "ui://w9mypx6jasm2u";

		public static UI_SuiPianDuiHuanWnd CreateInstance()
		{
			return (UI_SuiPianDuiHuanWnd)UIPackage.CreateObject("UI_Shop","SuiPianDuiHuanWnd");
		}

		public UI_SuiPianDuiHuanWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_suiPianExchangePV)this.GetChildAt(1);
		}
	}
}