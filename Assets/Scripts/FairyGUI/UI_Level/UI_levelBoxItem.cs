/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_levelBoxItem : GComponent
	{
		public GTextField m_topName;
		public GLoader m_iconLoader;
		public GGraph m_effectPos;
		public GGraph m_toucher;
		public GGroup m_teXiaoGroup;
		public Transition m_anim;

		public const string URL = "ui://z04ymz0ejlk31c";

		public static UI_levelBoxItem CreateInstance()
		{
			return (UI_levelBoxItem)UIPackage.CreateObject("UI_Level","levelBoxItem");
		}

		public UI_levelBoxItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_topName = (GTextField)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
			m_effectPos = (GGraph)this.GetChildAt(3);
			m_toucher = (GGraph)this.GetChildAt(4);
			m_teXiaoGroup = (GGroup)this.GetChildAt(9);
			m_anim = this.GetTransitionAt(0);
		}
	}
}