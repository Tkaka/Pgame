/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_NewChapterOpenWindow : GComponent
	{
		public GGraph m_mask;
		public GImage m_effect;
		public GImage m_effect_2;
		public Transition m_anim;

		public const string URL = "ui://z04ymz0eroor1s";

		public static UI_NewChapterOpenWindow CreateInstance()
		{
			return (UI_NewChapterOpenWindow)UIPackage.CreateObject("UI_Level","NewChapterOpenWindow");
		}

		public UI_NewChapterOpenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_effect = (GImage)this.GetChildAt(1);
			m_effect_2 = (GImage)this.GetChildAt(2);
			m_anim = this.GetTransitionAt(0);
		}
	}
}