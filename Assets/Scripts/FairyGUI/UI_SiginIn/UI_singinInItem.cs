/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SiginIn
{
	public partial class UI_singinInItem : GComponent
	{
		public GButton m_item;
		public GGroup m_getedGroup;
		public GTextField m_vipNum;
		public GGroup m_vipDoubleGroup;
		public GGraph m_toucher;
		public GTextField m_getAgainTip;

		public const string URL = "ui://jbviry4zdaur2";

		public static UI_singinInItem CreateInstance()
		{
			return (UI_singinInItem)UIPackage.CreateObject("UI_SiginIn","singinInItem");
		}

		public UI_singinInItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_item = (GButton)this.GetChildAt(1);
			m_getedGroup = (GGroup)this.GetChildAt(4);
			m_vipNum = (GTextField)this.GetChildAt(6);
			m_vipDoubleGroup = (GGroup)this.GetChildAt(8);
			m_toucher = (GGraph)this.GetChildAt(9);
			m_getAgainTip = (GTextField)this.GetChildAt(10);
		}
	}
}