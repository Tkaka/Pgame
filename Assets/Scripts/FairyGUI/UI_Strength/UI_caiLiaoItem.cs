/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_caiLiaoItem : GComponent
	{
		public GLoader m_borderBg;
		public GLoader m_caiLiaoIcon;
		public GImage m_addIcon;
		public GGroup m_unFullGroup;
		public GTextField m_numLabel;
		public GGraph m_itemBtn;

		public const string URL = "ui://qnd9tp35qsohg";

		public static UI_caiLiaoItem CreateInstance()
		{
			return (UI_caiLiaoItem)UIPackage.CreateObject("UI_Strength","caiLiaoItem");
		}

		public UI_caiLiaoItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderBg = (GLoader)this.GetChildAt(0);
			m_caiLiaoIcon = (GLoader)this.GetChildAt(1);
			m_addIcon = (GImage)this.GetChildAt(3);
			m_unFullGroup = (GGroup)this.GetChildAt(4);
			m_numLabel = (GTextField)this.GetChildAt(5);
			m_itemBtn = (GGraph)this.GetChildAt(6);
		}
	}
}