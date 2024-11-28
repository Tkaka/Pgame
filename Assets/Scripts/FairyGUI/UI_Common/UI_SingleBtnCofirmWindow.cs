/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_SingleBtnCofirmWindow : GComponent
	{
		public GTextField m_txtTitle;
		public GButton m_btnComfirm;
		public GButton m_btnClose;
		public GTextField m_txtDescribe;

		public const string URL = "ui://42sthz3tsmavxkx";

		public static UI_SingleBtnCofirmWindow CreateInstance()
		{
			return (UI_SingleBtnCofirmWindow)UIPackage.CreateObject("UI_Common","SingleBtnCofirmWindow");
		}

		public UI_SingleBtnCofirmWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtTitle = (GTextField)this.GetChildAt(1);
			m_btnComfirm = (GButton)this.GetChildAt(2);
			m_btnClose = (GButton)this.GetChildAt(3);
			m_txtDescribe = (GTextField)this.GetChildAt(5);
		}
	}
}