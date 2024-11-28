/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_WorldMap
{
	public partial class UI_SelectMissionWindow : GComponent
	{
		public UI_MissionPanel m_panel;

		public const string URL = "ui://k1lxoe22sbfsg";

		public static UI_SelectMissionWindow CreateInstance()
		{
			return (UI_SelectMissionWindow)UIPackage.CreateObject("UI_WorldMap","SelectMissionWindow");
		}

		public UI_SelectMissionWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_panel = (UI_MissionPanel)this.GetChildAt(0);
		}
	}
}