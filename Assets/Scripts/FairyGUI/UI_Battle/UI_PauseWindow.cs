/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_PauseWindow : GComponent
	{
		public GButton m_goonBtn;
		public GButton m_levelBtn;
		public GButton m_closeBtn;

		public const string URL = "ui://028ppdzhjh5a6j";

		public static UI_PauseWindow CreateInstance()
		{
			return (UI_PauseWindow)UIPackage.CreateObject("UI_Battle","PauseWindow");
		}

		public UI_PauseWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_goonBtn = (GButton)this.GetChildAt(6);
			m_levelBtn = (GButton)this.GetChildAt(7);
			m_closeBtn = (GButton)this.GetChildAt(14);
		}
	}
}