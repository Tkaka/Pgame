/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_VIP
{
	public partial class UI_RechargeWnd : GComponent
	{
		public GComponent m_commonTop;
		public GList m_rechargeList;
		public UI_VipTitle m_vipTitle;

		public const string URL = "ui://7pvd5vi49wdbh";

		public static UI_RechargeWnd CreateInstance()
		{
			return (UI_RechargeWnd)UIPackage.CreateObject("UI_VIP","RechargeWnd");
		}

		public UI_RechargeWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_commonTop = (GComponent)this.GetChildAt(1);
			m_rechargeList = (GList)this.GetChildAt(6);
			m_vipTitle = (UI_VipTitle)this.GetChildAt(7);
		}
	}
}