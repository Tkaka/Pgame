/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_ResolveFastSelectWnd : GComponent
	{
		public GButton m_close;
		public GButton m_centerYes;
		public GTextField m_txtTitle;
		public GButton m_checkBai;
		public GButton m_checkLv;
		public GButton m_checkLan;
		public GButton m_checkZi;
		public GGroup m_panelGroup;

		public const string URL = "ui://vexa0xryg5im14";

		public static UI_ResolveFastSelectWnd CreateInstance()
		{
			return (UI_ResolveFastSelectWnd)UIPackage.CreateObject("UI_AoYi","ResolveFastSelectWnd");
		}

		public UI_ResolveFastSelectWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_close = (GButton)this.GetChildAt(3);
			m_centerYes = (GButton)this.GetChildAt(6);
			m_txtTitle = (GTextField)this.GetChildAt(7);
			m_checkBai = (GButton)this.GetChildAt(8);
			m_checkLv = (GButton)this.GetChildAt(10);
			m_checkLan = (GButton)this.GetChildAt(12);
			m_checkZi = (GButton)this.GetChildAt(14);
			m_panelGroup = (GGroup)this.GetChildAt(16);
		}
	}
}