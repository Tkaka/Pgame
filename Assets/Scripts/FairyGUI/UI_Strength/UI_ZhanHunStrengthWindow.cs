/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_ZhanHunStrengthWindow : GComponent
	{
		public GGraph m_mask;
		public UI_zhanHunStrengthPV m_popupView;

		public const string URL = "ui://qnd9tp35ptxx28";

		public static UI_ZhanHunStrengthWindow CreateInstance()
		{
			return (UI_ZhanHunStrengthWindow)UIPackage.CreateObject("UI_Strength","ZhanHunStrengthWindow");
		}

		public UI_ZhanHunStrengthWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_zhanHunStrengthPV)this.GetChildAt(1);
		}
	}
}