/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_mainTopItem : GComponent
	{
		public GLoader m_levelFramLoader;
		public GLoader m_iconLoader;
		public UI_bubble m_bubble;
		public GLoader m_star1;
		public GLoader m_star2;
		public GLoader m_star3;
		public GGroup m_starList;
		public GTextField m_nameLabel;
		public GGroup m_item;

		public const string URL = "ui://z04ymz0eiv1n25i";

		public static UI_mainTopItem CreateInstance()
		{
			return (UI_mainTopItem)UIPackage.CreateObject("UI_Level","mainTopItem");
		}

		public UI_mainTopItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_levelFramLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_bubble = (UI_bubble)this.GetChildAt(2);
			m_star1 = (GLoader)this.GetChildAt(3);
			m_star2 = (GLoader)this.GetChildAt(4);
			m_star3 = (GLoader)this.GetChildAt(5);
			m_starList = (GGroup)this.GetChildAt(6);
			m_nameLabel = (GTextField)this.GetChildAt(8);
			m_item = (GGroup)this.GetChildAt(9);
		}
	}
}