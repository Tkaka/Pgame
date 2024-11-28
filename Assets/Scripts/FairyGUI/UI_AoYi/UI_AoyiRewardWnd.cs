/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiRewardWnd : GComponent
	{
		public GList m_mainList;
		public GButton m_btnClose;
		public GButton m_btnOneKeyGet;

		public const string URL = "ui://vexa0xrydys1w";

		public static UI_AoyiRewardWnd CreateInstance()
		{
			return (UI_AoyiRewardWnd)UIPackage.CreateObject("UI_AoYi","AoyiRewardWnd");
		}

		public UI_AoyiRewardWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mainList = (GList)this.GetChildAt(5);
			m_btnClose = (GButton)this.GetChildAt(6);
			m_btnOneKeyGet = (GButton)this.GetChildAt(7);
		}
	}
}