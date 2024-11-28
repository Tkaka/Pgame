/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_equipItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public UI_StarList m_starList;
		public GImage m_upArrowIcon;
		public UI_petQualityDou m_petQualityDou;
		public GLoader m_jiBanIconLoader;
		public GTextField m_levelLabel;
		public GImage m_selectIcon;
		public GImage m_redPoint;
		public GGroup m_lockGroup;
		public GGraph m_equipItemToucher;
		public Transition m_upArrowAnim;

		public const string URL = "ui://42sthz3tvs5zxqs";

		public static UI_equipItem CreateInstance()
		{
			return (UI_equipItem)UIPackage.CreateObject("UI_Common","equipItem");
		}

		public UI_equipItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_starList = (UI_StarList)this.GetChildAt(2);
			m_upArrowIcon = (GImage)this.GetChildAt(3);
			m_petQualityDou = (UI_petQualityDou)this.GetChildAt(4);
			m_jiBanIconLoader = (GLoader)this.GetChildAt(5);
			m_levelLabel = (GTextField)this.GetChildAt(7);
			m_selectIcon = (GImage)this.GetChildAt(8);
			m_redPoint = (GImage)this.GetChildAt(9);
			m_lockGroup = (GGroup)this.GetChildAt(12);
			m_equipItemToucher = (GGraph)this.GetChildAt(13);
			m_upArrowAnim = this.GetTransitionAt(0);
		}
	}
}