/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_ShopCommonList : GComponent
	{
		public GList m_mainList;
		public GButton m_btnLeft;
		public GButton m_btnRight;

		public const string URL = "ui://w9mypx6jhsph1a";

		public static UI_ShopCommonList CreateInstance()
		{
			return (UI_ShopCommonList)UIPackage.CreateObject("UI_Shop","ShopCommonList");
		}

		public UI_ShopCommonList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mainList = (GList)this.GetChildAt(0);
			m_btnLeft = (GButton)this.GetChildAt(1);
			m_btnRight = (GButton)this.GetChildAt(2);
		}
	}
}