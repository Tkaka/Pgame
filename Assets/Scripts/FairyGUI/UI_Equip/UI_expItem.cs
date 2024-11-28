/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_expItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GImage m_addIcon;
		public GGroup m_noHaveGroup;
		public GGraph m_itemToucher;
		public GTextField m_expValueLabel;
		public GTextField m_goldLabel;
		public GTextField m_numLabel;

		public const string URL = "ui://8u3gv94nd5g9x";

		public static UI_expItem CreateInstance()
		{
			return (UI_expItem)UIPackage.CreateObject("UI_Equip","expItem");
		}

		public UI_expItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_addIcon = (GImage)this.GetChildAt(3);
			m_noHaveGroup = (GGroup)this.GetChildAt(4);
			m_itemToucher = (GGraph)this.GetChildAt(5);
			m_expValueLabel = (GTextField)this.GetChildAt(6);
			m_goldLabel = (GTextField)this.GetChildAt(8);
			m_numLabel = (GTextField)this.GetChildAt(9);
		}
	}
}