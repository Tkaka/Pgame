/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_CheckMemberJoinWnd : GComponent
	{
		public GList m_apppyerList;
		public GTextField m_txtLimit;
		public UI_btnOneKeyRefuse m_btnOneKeyRefuse;
		public UI_btnZhaoMu m_btnZhaoMu;
		public UI_btnSetLimit m_btnSetLimit;
		public GTextField m_txtNoMember;

		public const string URL = "ui://oe7ras64fde92e";

		public static UI_CheckMemberJoinWnd CreateInstance()
		{
			return (UI_CheckMemberJoinWnd)UIPackage.CreateObject("UI_Guild","CheckMemberJoinWnd");
		}

		public UI_CheckMemberJoinWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_apppyerList = (GList)this.GetChildAt(4);
			m_txtLimit = (GTextField)this.GetChildAt(5);
			m_btnOneKeyRefuse = (UI_btnOneKeyRefuse)this.GetChildAt(6);
			m_btnZhaoMu = (UI_btnZhaoMu)this.GetChildAt(7);
			m_btnSetLimit = (UI_btnSetLimit)this.GetChildAt(8);
			m_txtNoMember = (GTextField)this.GetChildAt(9);
		}
	}
}