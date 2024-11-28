/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_CheckMemberCell : GComponent
	{
		public UI_headIcon m_icon;
		public GTextField m_txtName;
		public GTextField m_txtLevel;
		public GTextField m_txtTime;
		public UI_btnAgree m_btnAgree;
		public UI_btnRefuse m_btnRefuse;

		public const string URL = "ui://oe7ras64fde92f";

		public static UI_CheckMemberCell CreateInstance()
		{
			return (UI_CheckMemberCell)UIPackage.CreateObject("UI_Guild","CheckMemberCell");
		}

		public UI_CheckMemberCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_icon = (UI_headIcon)this.GetChildAt(1);
			m_txtName = (GTextField)this.GetChildAt(2);
			m_txtLevel = (GTextField)this.GetChildAt(3);
			m_txtTime = (GTextField)this.GetChildAt(4);
			m_btnAgree = (UI_btnAgree)this.GetChildAt(5);
			m_btnRefuse = (UI_btnRefuse)this.GetChildAt(6);
		}
	}
}