/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_equipPropItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GImage m_addIcon;
		public GGroup m_unFullGroup;
		public GGraph m_itemToucher;
		public GTextField m_numLabel;

		public const string URL = "ui://8u3gv94nt5fac";

		public static UI_equipPropItem CreateInstance()
		{
			return (UI_equipPropItem)UIPackage.CreateObject("UI_Equip","equipPropItem");
		}

		public UI_equipPropItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_addIcon = (GImage)this.GetChildAt(3);
			m_unFullGroup = (GGroup)this.GetChildAt(4);
			m_itemToucher = (GGraph)this.GetChildAt(5);
			m_numLabel = (GTextField)this.GetChildAt(6);
		}
	}
}