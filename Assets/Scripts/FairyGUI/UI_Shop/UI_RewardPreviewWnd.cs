/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_RewardPreviewWnd : GComponent
	{
		public GGraph m_mask;
		public UI_rewardPreviewPV m_popupView;

		public const string URL = "ui://w9mypx6jiy9s16";

		public static UI_RewardPreviewWnd CreateInstance()
		{
			return (UI_RewardPreviewWnd)UIPackage.CreateObject("UI_Shop","RewardPreviewWnd");
		}

		public UI_RewardPreviewWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_rewardPreviewPV)this.GetChildAt(1);
		}
	}
}