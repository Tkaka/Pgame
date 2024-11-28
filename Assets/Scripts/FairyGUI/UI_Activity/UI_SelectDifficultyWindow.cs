/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_SelectDifficultyWindow : GComponent
	{
		public GGraph m_mask;
		public UI_selectDifficultyPV m_popupView;

		public const string URL = "ui://zwmeip9ukrhbs";

		public static UI_SelectDifficultyWindow CreateInstance()
		{
			return (UI_SelectDifficultyWindow)UIPackage.CreateObject("UI_Activity","SelectDifficultyWindow");
		}

		public UI_SelectDifficultyWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_selectDifficultyPV)this.GetChildAt(1);
		}
	}
}