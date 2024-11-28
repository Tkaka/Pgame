/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_zhanHunCaiLiaoItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_itemIconLoader;
		public GImage m_fullUseIcon;
		public GTextField m_numLabel;
		public GGraph m_itemToucher;
		public GLoader m_selectIcon;

		public const string URL = "ui://qnd9tp35ptxx2d";

		public static UI_zhanHunCaiLiaoItem CreateInstance()
		{
			return (UI_zhanHunCaiLiaoItem)UIPackage.CreateObject("UI_Strength","zhanHunCaiLiaoItem");
		}

		public UI_zhanHunCaiLiaoItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_itemIconLoader = (GLoader)this.GetChildAt(1);
			m_fullUseIcon = (GImage)this.GetChildAt(2);
			m_numLabel = (GTextField)this.GetChildAt(3);
			m_itemToucher = (GGraph)this.GetChildAt(4);
			m_selectIcon = (GLoader)this.GetChildAt(5);
		}
	}
}