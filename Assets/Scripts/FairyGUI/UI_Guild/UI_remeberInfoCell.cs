/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_remeberInfoCell : GComponent
	{
		public UI_headIcon m_icon;
		public GTextField m_txtName;
		public GTextField m_txtLevel;
		public GTextField m_txtJob;
		public GTextField m_txtTime;
		public GTextField m_txtTotalContribution;
		public GTextField m_txtTodayContribution;

		public const string URL = "ui://oe7ras64qbwu1x";

		public static UI_remeberInfoCell CreateInstance()
		{
			return (UI_remeberInfoCell)UIPackage.CreateObject("UI_Guild","remeberInfoCell");
		}

		public UI_remeberInfoCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_icon = (UI_headIcon)this.GetChildAt(1);
			m_txtName = (GTextField)this.GetChildAt(2);
			m_txtLevel = (GTextField)this.GetChildAt(3);
			m_txtJob = (GTextField)this.GetChildAt(4);
			m_txtTime = (GTextField)this.GetChildAt(5);
			m_txtTotalContribution = (GTextField)this.GetChildAt(6);
			m_txtTodayContribution = (GTextField)this.GetChildAt(7);
		}
	}
}