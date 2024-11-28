/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SiginIn
{
	public partial class UI_siginInPV : GComponent
	{
		public GButton m_closeBtn;
		public GList m_timesRewardList;
		public GButton m_unGetBtn;
		public GButton m_getBtn;
		public GList m_siginRewardList;
		public GTextField m_leiJiRewardTip;
		public GTextField m_leiJiSiginUpTip;

		public const string URL = "ui://jbviry4zdaur3";

		public static UI_siginInPV CreateInstance()
		{
			return (UI_siginInPV)UIPackage.CreateObject("UI_SiginIn","siginInPV");
		}

		public UI_siginInPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GButton)this.GetChildAt(6);
			m_timesRewardList = (GList)this.GetChildAt(9);
			m_unGetBtn = (GButton)this.GetChildAt(10);
			m_getBtn = (GButton)this.GetChildAt(11);
			m_siginRewardList = (GList)this.GetChildAt(12);
			m_leiJiRewardTip = (GTextField)this.GetChildAt(14);
			m_leiJiSiginUpTip = (GTextField)this.GetChildAt(16);
		}
	}
}