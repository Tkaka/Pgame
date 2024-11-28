/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_zhanHunItem : GComponent
	{
		public GLoader m_zhanHunIconLoader;
		public GImage m_lockIcon;
		public GImage m_selectedIcon;
		public GGraph m_itemToucher;
		public GTextField m_lvLabel;
		public GGroup m_lvGroup;

		public const string URL = "ui://qnd9tp35swzn22";

		public static UI_zhanHunItem CreateInstance()
		{
			return (UI_zhanHunItem)UIPackage.CreateObject("UI_Strength","zhanHunItem");
		}

		public UI_zhanHunItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_zhanHunIconLoader = (GLoader)this.GetChildAt(1);
			m_lockIcon = (GImage)this.GetChildAt(2);
			m_selectedIcon = (GImage)this.GetChildAt(3);
			m_itemToucher = (GGraph)this.GetChildAt(4);
			m_lvLabel = (GTextField)this.GetChildAt(6);
			m_lvGroup = (GGroup)this.GetChildAt(7);
		}
	}
}