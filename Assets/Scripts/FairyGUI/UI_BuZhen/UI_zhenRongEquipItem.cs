/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_zhenRongEquipItem : GComponent
	{
		public GLoader m_boardLoader;
		public GTextField m_levelLabel;
		public GLoader m_iconLoader;
		public GComponent m_starList;
		public GGraph m_redPoint;
		public GImage m_upArrowIcon;
		public GLoader m_jiBanIconLoader;
		public GGraph m_equipItemToucher;
		public GImage m_selectIcon;
		public Transition m_upArrowAnim;

		public const string URL = "ui://z0csav43p95223";

		public static UI_zhenRongEquipItem CreateInstance()
		{
			return (UI_zhenRongEquipItem)UIPackage.CreateObject("UI_BuZhen","zhenRongEquipItem");
		}

		public UI_zhenRongEquipItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_levelLabel = (GTextField)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
			m_starList = (GComponent)this.GetChildAt(3);
			m_redPoint = (GGraph)this.GetChildAt(4);
			m_upArrowIcon = (GImage)this.GetChildAt(5);
			m_jiBanIconLoader = (GLoader)this.GetChildAt(6);
			m_equipItemToucher = (GGraph)this.GetChildAt(7);
			m_selectIcon = (GImage)this.GetChildAt(8);
			m_upArrowAnim = this.GetTransitionAt(0);
		}
	}
}