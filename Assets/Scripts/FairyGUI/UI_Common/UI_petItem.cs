/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_petItem : GButton
	{
		public GLoader m_borderBg;
		public GLoader m_iconLoader;
		public UI_StarList m_starList;
		public GTextField m_petName;
		public UI_petQualityDou m_petQualityDou;
		public GGroup m_shangZhenGroup;
		public GTextField m_levelLabel;
		public GLoader m_selectIcon;
		public GImage m_redPoint;

		public const string URL = "ui://42sthz3tvds0jw";

		public static UI_petItem CreateInstance()
		{
			return (UI_petItem)UIPackage.CreateObject("UI_Common","petItem");
		}

		public UI_petItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderBg = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_starList = (UI_StarList)this.GetChildAt(2);
			m_petName = (GTextField)this.GetChildAt(3);
			m_petQualityDou = (UI_petQualityDou)this.GetChildAt(4);
			m_shangZhenGroup = (GGroup)this.GetChildAt(7);
			m_levelLabel = (GTextField)this.GetChildAt(9);
			m_selectIcon = (GLoader)this.GetChildAt(10);
			m_redPoint = (GImage)this.GetChildAt(11);
		}
	}
}