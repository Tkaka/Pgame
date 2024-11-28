/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_mainLevelItem : GButton
	{
		public GImage m_diZuoLoader;
		public GGraph m_effectPos;
		public GTextField m_tipText;
		public UI_mainTopItem m_topItem;
		public GGraph m_toucher;
		public Transition m_anim;

		public const string URL = "ui://z04ymz0e97kmh";

		public static UI_mainLevelItem CreateInstance()
		{
			return (UI_mainLevelItem)UIPackage.CreateObject("UI_Level","mainLevelItem");
		}

		public UI_mainLevelItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_diZuoLoader = (GImage)this.GetChildAt(0);
			m_effectPos = (GGraph)this.GetChildAt(1);
			m_tipText = (GTextField)this.GetChildAt(2);
			m_topItem = (UI_mainTopItem)this.GetChildAt(3);
			m_toucher = (GGraph)this.GetChildAt(4);
			m_anim = this.GetTransitionAt(0);
		}
	}
}