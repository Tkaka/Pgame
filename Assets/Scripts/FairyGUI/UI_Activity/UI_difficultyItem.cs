/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_difficultyItem : GComponent
	{
		public GLoader m_bgLoader;
		public GGraph m_toucher;
		public GButton m_sweepBtn;
		public GImage m_lockIcon;
		public Transition m_anim;

		public const string URL = "ui://zwmeip9ukrhbu";

		public static UI_difficultyItem CreateInstance()
		{
			return (UI_difficultyItem)UIPackage.CreateObject("UI_Activity","difficultyItem");
		}

		public UI_difficultyItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bgLoader = (GLoader)this.GetChildAt(0);
			m_toucher = (GGraph)this.GetChildAt(1);
			m_sweepBtn = (GButton)this.GetChildAt(2);
			m_lockIcon = (GImage)this.GetChildAt(3);
			m_anim = this.GetTransitionAt(0);
		}
	}
}