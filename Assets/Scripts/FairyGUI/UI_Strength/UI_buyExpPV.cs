/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_buyExpPV : GComponent
	{
		public GButton m_addBtn;
		public GButton m_reduceBtn;
		public GButton m_maxBtn;
		public GButton m_closeBtn;
		public GTextField m_numLabel;
		public GTextField m_priceLabel;
		public GLoader m_itemBorderLoader;
		public GLoader m_itemIconLoader;
		public GTextField m_nameLabel;
		public GTextField m_valueLabel;
		public GButton m_buyBtn;

		public const string URL = "ui://qnd9tp35lxvz4l";

		public static UI_buyExpPV CreateInstance()
		{
			return (UI_buyExpPV)UIPackage.CreateObject("UI_Strength","buyExpPV");
		}

		public UI_buyExpPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_addBtn = (GButton)this.GetChildAt(6);
			m_reduceBtn = (GButton)this.GetChildAt(7);
			m_maxBtn = (GButton)this.GetChildAt(8);
			m_closeBtn = (GButton)this.GetChildAt(9);
			m_numLabel = (GTextField)this.GetChildAt(11);
			m_priceLabel = (GTextField)this.GetChildAt(13);
			m_itemBorderLoader = (GLoader)this.GetChildAt(14);
			m_itemIconLoader = (GLoader)this.GetChildAt(15);
			m_nameLabel = (GTextField)this.GetChildAt(16);
			m_valueLabel = (GTextField)this.GetChildAt(17);
			m_buyBtn = (GButton)this.GetChildAt(19);
		}
	}
}