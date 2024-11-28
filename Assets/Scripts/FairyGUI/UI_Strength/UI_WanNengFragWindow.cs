/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_WanNengFragWindow : GComponent
	{
		public GGraph m_mask;
		public UI_wanNengFragPV m_popupView;

		public const string URL = "ui://qnd9tp35n1c02k";

		public static UI_WanNengFragWindow CreateInstance()
		{
			return (UI_WanNengFragWindow)UIPackage.CreateObject("UI_Strength","WanNengFragWindow");
		}

		public UI_WanNengFragWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_wanNengFragPV)this.GetChildAt(1);
		}
	}
}