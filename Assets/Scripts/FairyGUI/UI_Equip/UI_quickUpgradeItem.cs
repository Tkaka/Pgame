/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_quickUpgradeItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GTextField m_numLabel;
		public GGraph m_itemToucher;
		public GImage m_addIcon;
		public GGroup m_unFullGroup;

		public const string URL = "ui://8u3gv94nd5g911";

		public static UI_quickUpgradeItem CreateInstance()
		{
			return (UI_quickUpgradeItem)UIPackage.CreateObject("UI_Equip","quickUpgradeItem");
		}

		public UI_quickUpgradeItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_numLabel = (GTextField)this.GetChildAt(2);
			m_itemToucher = (GGraph)this.GetChildAt(3);
			m_addIcon = (GImage)this.GetChildAt(5);
			m_unFullGroup = (GGroup)this.GetChildAt(6);
		}
	}
}