/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_peiYangResItem : GComponent
	{
		public GImage m_bg;
		public GButton m_doubleSelectBtn;
		public GImage m_recommendIcon;
		public UI_peiYangResAttrItem m_peiYangResAttrItem1;
		public UI_peiYangResAttrItem m_peiYangResAttrItem2;
		public UI_peiYangResAttrItem m_peiYangResAttrItem3;

		public const string URL = "ui://bi2nkn43fd9ij";

		public static UI_peiYangResItem CreateInstance()
		{
			return (UI_peiYangResItem)UIPackage.CreateObject("UI_ShenQi","peiYangResItem");
		}

		public UI_peiYangResItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GImage)this.GetChildAt(0);
			m_doubleSelectBtn = (GButton)this.GetChildAt(1);
			m_recommendIcon = (GImage)this.GetChildAt(2);
			m_peiYangResAttrItem1 = (UI_peiYangResAttrItem)this.GetChildAt(3);
			m_peiYangResAttrItem2 = (UI_peiYangResAttrItem)this.GetChildAt(4);
			m_peiYangResAttrItem3 = (UI_peiYangResAttrItem)this.GetChildAt(5);
		}
	}
}