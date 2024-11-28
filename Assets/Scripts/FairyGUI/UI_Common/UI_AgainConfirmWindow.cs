/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_AgainConfirmWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_close;
		public GButton m_cancelBtn;
		public GButton m_confirmBtn;
		public GRichTextField m_tipLabel;
		public GButton m_centerYes;
		public GTextField m_txtTitle;
		public GGroup m_panelGroup;

		public const string URL = "ui://42sthz3tsmavxkw";

		public static UI_AgainConfirmWindow CreateInstance()
		{
			return (UI_AgainConfirmWindow)UIPackage.CreateObject("UI_Common","AgainConfirmWindow");
		}

		public UI_AgainConfirmWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_close = (GButton)this.GetChildAt(4);
			m_cancelBtn = (GButton)this.GetChildAt(7);
			m_confirmBtn = (GButton)this.GetChildAt(8);
			m_tipLabel = (GRichTextField)this.GetChildAt(9);
			m_centerYes = (GButton)this.GetChildAt(10);
			m_txtTitle = (GTextField)this.GetChildAt(11);
			m_panelGroup = (GGroup)this.GetChildAt(12);
		}
	}
}