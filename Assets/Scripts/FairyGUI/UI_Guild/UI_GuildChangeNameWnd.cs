/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_GuildChangeNameWnd : GComponent
	{
		public GButton m_btnClose;
		public UI_btnOk m_btnOk;
		public GTextInput m_txtInput;
		public GTextField m_txtDiamondNum;

		public const string URL = "ui://oe7ras64fde925";

		public static UI_GuildChangeNameWnd CreateInstance()
		{
			return (UI_GuildChangeNameWnd)UIPackage.CreateObject("UI_Guild","GuildChangeNameWnd");
		}

		public UI_GuildChangeNameWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_btnOk = (UI_btnOk)this.GetChildAt(4);
			m_txtInput = (GTextInput)this.GetChildAt(6);
			m_txtDiamondNum = (GTextField)this.GetChildAt(9);
		}
	}
}