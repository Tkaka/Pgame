/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objDonatePageCell : GComponent
	{
		public GLoader m_imgIcon;
		public GTextField m_txtName;
		public GTextField m_txtLevel;
		public UI_objProgressBar3 m_progressBar;
		public GTextField m_txtCurLevelDes;
		public GTextField m_txtNextLevelDes;
		public UI_btnDonate m_btnDonate;
		public GGroup m_openGroup;
		public GTextField m_objNoOpen;

		public const string URL = "ui://oe7ras64nos7b47";

		public static UI_objDonatePageCell CreateInstance()
		{
			return (UI_objDonatePageCell)UIPackage.CreateObject("UI_Guild","objDonatePageCell");
		}

		public UI_objDonatePageCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcon = (GLoader)this.GetChildAt(2);
			m_txtName = (GTextField)this.GetChildAt(3);
			m_txtLevel = (GTextField)this.GetChildAt(4);
			m_progressBar = (UI_objProgressBar3)this.GetChildAt(5);
			m_txtCurLevelDes = (GTextField)this.GetChildAt(10);
			m_txtNextLevelDes = (GTextField)this.GetChildAt(11);
			m_btnDonate = (UI_btnDonate)this.GetChildAt(12);
			m_openGroup = (GGroup)this.GetChildAt(13);
			m_objNoOpen = (GTextField)this.GetChildAt(14);
		}
	}
}