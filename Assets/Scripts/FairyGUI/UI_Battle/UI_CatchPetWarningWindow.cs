/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_CatchPetWarningWindow : GComponent
	{
		public GGroup m_topBar;
		public GGroup m_btmBar;
		public GGroup m_crosser;
		public Transition m_ani;

		public const string URL = "ui://028ppdzhw9a463";

		public static UI_CatchPetWarningWindow CreateInstance()
		{
			return (UI_CatchPetWarningWindow)UIPackage.CreateObject("UI_Battle","CatchPetWarningWindow");
		}

		public UI_CatchPetWarningWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_topBar = (GGroup)this.GetChildAt(7);
			m_btmBar = (GGroup)this.GetChildAt(14);
			m_crosser = (GGroup)this.GetChildAt(26);
			m_ani = this.GetTransitionAt(0);
		}
	}
}