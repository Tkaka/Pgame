/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_objCoinItemCell : GComponent
	{
		public GTextField m_txtName;
		public GTextField m_num;
		public GButton m_itemIcon;

		public const string URL = "ui://w9mypx6jijxi22";

		public static UI_objCoinItemCell CreateInstance()
		{
			return (UI_objCoinItemCell)UIPackage.CreateObject("UI_Shop","objCoinItemCell");
		}

		public UI_objCoinItemCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtName = (GTextField)this.GetChildAt(0);
			m_num = (GTextField)this.GetChildAt(1);
			m_itemIcon = (GButton)this.GetChildAt(3);
		}
	}
}