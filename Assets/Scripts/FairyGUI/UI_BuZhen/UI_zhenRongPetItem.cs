/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_zhenRongPetItem : GComponent
	{
		public GButton m_petItem;
		public GGroup m_unGetGroup;
		public GGraph m_itemToucher;

		public const string URL = "ui://z0csav438uoo2o";

		public static UI_zhenRongPetItem CreateInstance()
		{
			return (UI_zhenRongPetItem)UIPackage.CreateObject("UI_BuZhen","zhenRongPetItem");
		}

		public UI_zhenRongPetItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_petItem = (GButton)this.GetChildAt(0);
			m_unGetGroup = (GGroup)this.GetChildAt(4);
			m_itemToucher = (GGraph)this.GetChildAt(5);
		}
	}
}