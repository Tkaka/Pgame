/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_cloneTeammateItem : GComponent
	{
		public GImage m_addIcon;
		public GImage m_emptyIcon;
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GTextField m_lvLabel;
		public GComponent m_starList;
		public GImage m_switchIcon;
		public GImage m_captainIcon;
		public GTextField m_nameLabel;
		public GTextField m_progressLabel;
		public GGroup m_teammateGroup;
		public GGraph m_toucher;

		public const string URL = "ui://y12h0jfmlyhni";

		public static UI_cloneTeammateItem CreateInstance()
		{
			return (UI_cloneTeammateItem)UIPackage.CreateObject("UI_CloneTeamFight","cloneTeammateItem");
		}

		public UI_cloneTeammateItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_addIcon = (GImage)this.GetChildAt(0);
			m_emptyIcon = (GImage)this.GetChildAt(1);
			m_boardLoader = (GLoader)this.GetChildAt(2);
			m_iconLoader = (GLoader)this.GetChildAt(3);
			m_lvLabel = (GTextField)this.GetChildAt(4);
			m_starList = (GComponent)this.GetChildAt(5);
			m_switchIcon = (GImage)this.GetChildAt(6);
			m_captainIcon = (GImage)this.GetChildAt(7);
			m_nameLabel = (GTextField)this.GetChildAt(9);
			m_progressLabel = (GTextField)this.GetChildAt(10);
			m_teammateGroup = (GGroup)this.GetChildAt(11);
			m_toucher = (GGraph)this.GetChildAt(12);
		}
	}
}