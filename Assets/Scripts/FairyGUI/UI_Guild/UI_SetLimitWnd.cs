/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_SetLimitWnd : GComponent
	{
		public GButton m_btnClose;
		public GComponent m_btnTypeRight;
		public GComponent m_btnTypeLeft;
		public GTextField m_txtType;
		public GComponent m_btnLevelRight;
		public GComponent m_btnLevelLeft;
		public GTextField m_txtLevel;
		public UI_btnOk m_btnOk;
		public UI_btnCancel m_btnCancel;

		public const string URL = "ui://oe7ras64fde92l";

		public static UI_SetLimitWnd CreateInstance()
		{
			return (UI_SetLimitWnd)UIPackage.CreateObject("UI_Guild","SetLimitWnd");
		}

		public UI_SetLimitWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_btnTypeRight = (GComponent)this.GetChildAt(4);
			m_btnTypeLeft = (GComponent)this.GetChildAt(5);
			m_txtType = (GTextField)this.GetChildAt(8);
			m_btnLevelRight = (GComponent)this.GetChildAt(9);
			m_btnLevelLeft = (GComponent)this.GetChildAt(10);
			m_txtLevel = (GTextField)this.GetChildAt(13);
			m_btnOk = (UI_btnOk)this.GetChildAt(15);
			m_btnCancel = (UI_btnCancel)this.GetChildAt(16);
		}
	}
}