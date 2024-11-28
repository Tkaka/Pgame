/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_TrumpetWnd : GComponent
	{
		public Controller m_TrumpetTypeControl;
		public Controller m_colorControl;
		public Controller m_fontControl;
		public Controller m_moveControl;
		public GButton m_btnClose;
		public GTextInput m_txtInput;
		public UI_btnSend m_btnSend;
		public GTextField m_txtTrumpetNum;
		public GTextField m_txtDiamondNum;
		public GTextField m_txtComsumeTrumpet;
		public UI_btnGift m_btnGift;
		public GButton m_btnFont1;
		public GButton m_btnFont2;
		public GButton m_btnMoveLeft;
		public GButton m_btnMoveLeftUp;
		public GButton m_btnMoveLeftDown;
		public UI_btnColor m_btnColor;
		public GComponent m_btnRight;
		public GList m_colorList;
		public GComponent m_btnLeft;
		public GGroup m_chatGroup;

		public const string URL = "ui://51gazvjdkb311l";

		public static UI_TrumpetWnd CreateInstance()
		{
			return (UI_TrumpetWnd)UIPackage.CreateObject("UI_Chat","TrumpetWnd");
		}

		public UI_TrumpetWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TrumpetTypeControl = this.GetControllerAt(0);
			m_colorControl = this.GetControllerAt(1);
			m_fontControl = this.GetControllerAt(2);
			m_moveControl = this.GetControllerAt(3);
			m_btnClose = (GButton)this.GetChildAt(2);
			m_txtInput = (GTextInput)this.GetChildAt(3);
			m_btnSend = (UI_btnSend)this.GetChildAt(4);
			m_txtTrumpetNum = (GTextField)this.GetChildAt(7);
			m_txtDiamondNum = (GTextField)this.GetChildAt(10);
			m_txtComsumeTrumpet = (GTextField)this.GetChildAt(17);
			m_btnGift = (UI_btnGift)this.GetChildAt(19);
			m_btnFont1 = (GButton)this.GetChildAt(21);
			m_btnFont2 = (GButton)this.GetChildAt(22);
			m_btnMoveLeft = (GButton)this.GetChildAt(23);
			m_btnMoveLeftUp = (GButton)this.GetChildAt(24);
			m_btnMoveLeftDown = (GButton)this.GetChildAt(25);
			m_btnColor = (UI_btnColor)this.GetChildAt(26);
			m_btnRight = (GComponent)this.GetChildAt(27);
			m_colorList = (GList)this.GetChildAt(29);
			m_btnLeft = (GComponent)this.GetChildAt(30);
			m_chatGroup = (GGroup)this.GetChildAt(33);
		}
	}
}