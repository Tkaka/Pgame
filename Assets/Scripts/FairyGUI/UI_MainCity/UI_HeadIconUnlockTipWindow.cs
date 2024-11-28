/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_HeadIconUnlockTipWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_closeBtn;
		public GComponent m_headIcon;
		public GTextField m_unlcokCoditionLabel;

		public const string URL = "ui://jdfufi06kho74x";

		public static UI_HeadIconUnlockTipWindow CreateInstance()
		{
			return (UI_HeadIconUnlockTipWindow)UIPackage.CreateObject("UI_MainCity","HeadIconUnlockTipWindow");
		}

		public UI_HeadIconUnlockTipWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_closeBtn = (GButton)this.GetChildAt(3);
			m_headIcon = (GComponent)this.GetChildAt(4);
			m_unlcokCoditionLabel = (GTextField)this.GetChildAt(6);
		}
	}
}