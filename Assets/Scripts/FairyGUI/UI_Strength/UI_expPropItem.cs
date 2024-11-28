/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_expPropItem : GComponent
	{
		public GLoader m_borderBg;
		public GLoader m_caiLiaoIcon;
		public GTextField m_number;
		public GTextField m_tipLabel;
		public GImage m_addIcon;
		public GGroup m_unFullGroup;
		public GGraph m_expToucher;

		public const string URL = "ui://qnd9tp35yq8as";

		public static UI_expPropItem CreateInstance()
		{
			return (UI_expPropItem)UIPackage.CreateObject("UI_Strength","expPropItem");
		}

		public UI_expPropItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderBg = (GLoader)this.GetChildAt(0);
			m_caiLiaoIcon = (GLoader)this.GetChildAt(1);
			m_number = (GTextField)this.GetChildAt(2);
			m_tipLabel = (GTextField)this.GetChildAt(3);
			m_addIcon = (GImage)this.GetChildAt(5);
			m_unFullGroup = (GGroup)this.GetChildAt(6);
			m_expToucher = (GGraph)this.GetChildAt(7);
		}
	}
}