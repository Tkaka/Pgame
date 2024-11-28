/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_ZHDiamondStrenthWindow : GComponent
	{
		public GGraph m_mask;
		public UI_ZHDiamondStrengthPV m_popupView;

		public const string URL = "ui://qnd9tp35y2qa30";

		public static UI_ZHDiamondStrenthWindow CreateInstance()
		{
			return (UI_ZHDiamondStrenthWindow)UIPackage.CreateObject("UI_Strength","ZHDiamondStrenthWindow");
		}

		public UI_ZHDiamondStrenthWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_ZHDiamondStrengthPV)this.GetChildAt(1);
		}
	}
}