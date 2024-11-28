/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_ChairmanRewardWnd : GComponent
	{
		public GButton m_btnClose;
		public UI_btnGet m_btnGet;
		public GTextField m_txtDes;
		public GList m_rewardList;
		public GComponent m_objGeted;

		public const string URL = "ui://oe7ras64f1jg39";

		public static UI_ChairmanRewardWnd CreateInstance()
		{
			return (UI_ChairmanRewardWnd)UIPackage.CreateObject("UI_Guild","ChairmanRewardWnd");
		}

		public UI_ChairmanRewardWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_btnGet = (UI_btnGet)this.GetChildAt(4);
			m_txtDes = (GTextField)this.GetChildAt(5);
			m_rewardList = (GList)this.GetChildAt(6);
			m_objGeted = (GComponent)this.GetChildAt(7);
		}
	}
}