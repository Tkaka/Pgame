/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_BuyExpWindow : GComponent
	{
		public GGraph m_mask;
		public UI_buyExpPV m_popupView;

		public const string URL = "ui://qnd9tp35p9fv1g";

		public static UI_BuyExpWindow CreateInstance()
		{
			return (UI_BuyExpWindow)UIPackage.CreateObject("UI_Strength","BuyExpWindow");
		}

		public UI_BuyExpWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_buyExpPV)this.GetChildAt(1);
		}
	}
}