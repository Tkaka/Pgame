/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_BagWindow : GComponent
	{
		public UI_bagToggleGroup m_bagToggleGroup;
		public GButton m_sellBtn;
		public GTextField m_desc;
		public GButton m_useBtn;
		public GTextField m_sellPrice;
		public GGroup m_canSell;
		public GTextField m_cannotSell;
		public GButton m_compoundBtn;
		public GRichTextField m_itemName;
		public GTextField m_itemNum;
		public GButton m_selectIcon;
		public GList m_allList;
		public GGroup m_content;
		public GTextField m_nullTip;
		public GComponent m_commonTop;

		public const string URL = "ui://g5pgln3na9bf15";

		public static UI_BagWindow CreateInstance()
		{
			return (UI_BagWindow)UIPackage.CreateObject("UI_Beibao","BagWindow");
		}

		public UI_BagWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bagToggleGroup = (UI_bagToggleGroup)this.GetChildAt(1);
			m_sellBtn = (GButton)this.GetChildAt(5);
			m_desc = (GTextField)this.GetChildAt(6);
			m_useBtn = (GButton)this.GetChildAt(7);
			m_sellPrice = (GTextField)this.GetChildAt(9);
			m_canSell = (GGroup)this.GetChildAt(11);
			m_cannotSell = (GTextField)this.GetChildAt(12);
			m_compoundBtn = (GButton)this.GetChildAt(13);
			m_itemName = (GRichTextField)this.GetChildAt(14);
			m_itemNum = (GTextField)this.GetChildAt(16);
			m_selectIcon = (GButton)this.GetChildAt(18);
			m_allList = (GList)this.GetChildAt(19);
			m_content = (GGroup)this.GetChildAt(20);
			m_nullTip = (GTextField)this.GetChildAt(21);
			m_commonTop = (GComponent)this.GetChildAt(24);
		}
	}
}