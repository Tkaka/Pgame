/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_WorldMap
{
	public partial class UI_MissionPanel : GComponent
	{
		public GButton m_enterBtn;
		public GButton m_closeBtn;
		public Transition m_t0;

		public const string URL = "ui://k1lxoe22tu9ok";

		public static UI_MissionPanel CreateInstance()
		{
			return (UI_MissionPanel)UIPackage.CreateObject("UI_WorldMap","MissionPanel");
		}

		public UI_MissionPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_enterBtn = (GButton)this.GetChildAt(1);
			m_closeBtn = (GButton)this.GetChildAt(2);
			m_t0 = this.GetTransitionAt(0);
		}
	}
}