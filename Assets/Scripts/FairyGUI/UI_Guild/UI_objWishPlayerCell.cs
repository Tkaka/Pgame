/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objWishPlayerCell : GComponent
	{
		public UI_headIcon m_imgIcon;
		public GTextField m_txtName;
		public GTextField m_txtLevel;
		public GComponent m_wished;
		public UI_btnWish m_btnWish;
		public UI_btnForHelp m_btnForHelp;
		public UI_btnGift m_btnGift;
		public GComponent m_btnAdd;
		public GGroup m_addGroup;
		public GButton m_ItemIcon;
		public GProgressBar m_progressBar;
		public GTextField m_txtProgress;
		public GTextField m_txtHaveNum;
		public GGroup m_itemGropu;

		public const string URL = "ui://oe7ras64vmh3b3m";

		public static UI_objWishPlayerCell CreateInstance()
		{
			return (UI_objWishPlayerCell)UIPackage.CreateObject("UI_Guild","objWishPlayerCell");
		}

		public UI_objWishPlayerCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcon = (UI_headIcon)this.GetChildAt(1);
			m_txtName = (GTextField)this.GetChildAt(2);
			m_txtLevel = (GTextField)this.GetChildAt(3);
			m_wished = (GComponent)this.GetChildAt(4);
			m_btnWish = (UI_btnWish)this.GetChildAt(5);
			m_btnForHelp = (UI_btnForHelp)this.GetChildAt(6);
			m_btnGift = (UI_btnGift)this.GetChildAt(7);
			m_btnAdd = (GComponent)this.GetChildAt(8);
			m_addGroup = (GGroup)this.GetChildAt(10);
			m_ItemIcon = (GButton)this.GetChildAt(11);
			m_progressBar = (GProgressBar)this.GetChildAt(12);
			m_txtProgress = (GTextField)this.GetChildAt(13);
			m_txtHaveNum = (GTextField)this.GetChildAt(14);
			m_itemGropu = (GGroup)this.GetChildAt(15);
		}
	}
}