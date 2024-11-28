/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiTipsWnd : GComponent
	{
		public GGraph m_mask;
		public GImage m_imgBg;
		public GImage m_txtBg;
		public GGroup m_bgGroup;
		public UI_AoyiCommonItem m_itemIcon;
		public GTextField m_txtName;
		public GTextField m_txtNum;
		public GGroup m_haveGroup;
		public GGroup m_titleGroup;
		public GTextField m_txtDescribe;
		public GGroup m_itemGroup;

		public const string URL = "ui://vexa0xrypgb21v";

		public static UI_AoyiTipsWnd CreateInstance()
		{
			return (UI_AoyiTipsWnd)UIPackage.CreateObject("UI_AoYi","AoyiTipsWnd");
		}

		public UI_AoyiTipsWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_imgBg = (GImage)this.GetChildAt(1);
			m_txtBg = (GImage)this.GetChildAt(2);
			m_bgGroup = (GGroup)this.GetChildAt(3);
			m_itemIcon = (UI_AoyiCommonItem)this.GetChildAt(4);
			m_txtName = (GTextField)this.GetChildAt(5);
			m_txtNum = (GTextField)this.GetChildAt(7);
			m_haveGroup = (GGroup)this.GetChildAt(8);
			m_titleGroup = (GGroup)this.GetChildAt(9);
			m_txtDescribe = (GTextField)this.GetChildAt(10);
			m_itemGroup = (GGroup)this.GetChildAt(11);
		}
	}
}