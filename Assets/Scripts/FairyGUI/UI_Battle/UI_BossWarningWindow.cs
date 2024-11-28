/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BossWarningWindow : GComponent
	{
		public GGroup m_topBar;
		public GGroup m_btmBar;
		public GGroup m_center;
		public Transition m_ani;

		public const string URL = "ui://028ppdzhnlgm5l";

		public static UI_BossWarningWindow CreateInstance()
		{
			return (UI_BossWarningWindow)UIPackage.CreateObject("UI_Battle","BossWarningWindow");
		}

		public UI_BossWarningWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_topBar = (GGroup)this.GetChildAt(7);
			m_btmBar = (GGroup)this.GetChildAt(14);
			m_center = (GGroup)this.GetChildAt(17);
			m_ani = this.GetTransitionAt(0);
		}
	}
}