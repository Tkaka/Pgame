/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_starBoxItem : GComponent
	{
		public GImage m_flashIcon;
		public GLoader m_iconLoader;
		public GGraph m_effectPos;
		public GImage m_redPoiint;
		public GGraph m_toucher;
		public GTextField m_normalStarNum;
		public GGroup m_normalStarGroup;
		public GTextField m_keyReceiveStarNum;
		public GGroup m_keyReceiveStarGroup;
		public Transition m_anim;

		public const string URL = "ui://z04ymz0e97kmr";

		public static UI_starBoxItem CreateInstance()
		{
			return (UI_starBoxItem)UIPackage.CreateObject("UI_Level","starBoxItem");
		}

		public UI_starBoxItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_flashIcon = (GImage)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_effectPos = (GGraph)this.GetChildAt(2);
			m_redPoiint = (GImage)this.GetChildAt(3);
			m_toucher = (GGraph)this.GetChildAt(4);
			m_normalStarNum = (GTextField)this.GetChildAt(6);
			m_normalStarGroup = (GGroup)this.GetChildAt(7);
			m_keyReceiveStarNum = (GTextField)this.GetChildAt(9);
			m_keyReceiveStarGroup = (GGroup)this.GetChildAt(10);
			m_anim = this.GetTransitionAt(0);
		}
	}
}