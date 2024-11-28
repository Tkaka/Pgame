/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_eliteLevelItem : GComponent
	{
		public GLoader m_levelFramLoader;
		public GLoader m_iconLoader;
		public GTextField m_nameLabel;
		public GLoader m_star1;
		public GLoader m_star2;
		public GLoader m_star3;
		public GGroup m_starList;
		public GGraph m_toucher;

		public const string URL = "ui://z04ymz0ejlk31b";

		public static UI_eliteLevelItem CreateInstance()
		{
			return (UI_eliteLevelItem)UIPackage.CreateObject("UI_Level","eliteLevelItem");
		}

		public UI_eliteLevelItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_levelFramLoader = (GLoader)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
			m_nameLabel = (GTextField)this.GetChildAt(4);
			m_star1 = (GLoader)this.GetChildAt(5);
			m_star2 = (GLoader)this.GetChildAt(6);
			m_star3 = (GLoader)this.GetChildAt(7);
			m_starList = (GGroup)this.GetChildAt(8);
			m_toucher = (GGraph)this.GetChildAt(9);
		}
	}
}