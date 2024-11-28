/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_MysteriousShopPV : GComponent
	{
		public GTextField m_txtTime;
		public GGroup m_refreshGroup;
		public UI_ShopCommonList m_objList;
		public GButton m_btnClose;

		public const string URL = "ui://w9mypx6jlxvz1x";

		public static UI_MysteriousShopPV CreateInstance()
		{
			return (UI_MysteriousShopPV)UIPackage.CreateObject("UI_Shop","MysteriousShopPV");
		}

		public UI_MysteriousShopPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtTime = (GTextField)this.GetChildAt(4);
			m_refreshGroup = (GGroup)this.GetChildAt(5);
			m_objList = (UI_ShopCommonList)this.GetChildAt(7);
			m_btnClose = (GButton)this.GetChildAt(8);
		}
	}
}