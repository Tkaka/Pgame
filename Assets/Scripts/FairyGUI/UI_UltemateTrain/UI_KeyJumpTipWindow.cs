/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_KeyJumpTipWindow : GComponent
	{
		public GButton m_tipShowBtn;
		public GTextField m_contentLabel;
		public GButton m_refuseBtn;
		public GButton m_jumpBtn;
		public GButton m_closeBtn;

		public const string URL = "ui://1wdkrtiuw0hud";

		public static UI_KeyJumpTipWindow CreateInstance()
		{
			return (UI_KeyJumpTipWindow)UIPackage.CreateObject("UI_UltemateTrain","KeyJumpTipWindow");
		}

		public UI_KeyJumpTipWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipShowBtn = (GButton)this.GetChildAt(7);
			m_contentLabel = (GTextField)this.GetChildAt(9);
			m_refuseBtn = (GButton)this.GetChildAt(10);
			m_jumpBtn = (GButton)this.GetChildAt(11);
			m_closeBtn = (GButton)this.GetChildAt(12);
		}
	}
}