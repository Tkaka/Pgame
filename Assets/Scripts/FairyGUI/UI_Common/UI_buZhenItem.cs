/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_buZhenItem : GButton
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public UI_StarList m_starList;
		public GGroup m_petInfoGroup;
		public GGroup m_noPetGroup;
		public GGraph m_toucher;

		public const string URL = "ui://42sthz3tkrhbk7";

		public static UI_buZhenItem CreateInstance()
		{
			return (UI_buZhenItem)UIPackage.CreateObject("UI_Common","buZhenItem");
		}

		public UI_buZhenItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_starList = (UI_StarList)this.GetChildAt(2);
			m_petInfoGroup = (GGroup)this.GetChildAt(3);
			m_noPetGroup = (GGroup)this.GetChildAt(7);
			m_toucher = (GGraph)this.GetChildAt(8);
		}
	}
}