/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_WeTaJiaSuWindow : GComponent
	{
		public GList m_petList;
		public GButton m_close;

		public const string URL = "ui://wwlsouxzp4koj";

		public static UI_GD_WeTaJiaSuWindow CreateInstance()
		{
			return (UI_GD_WeTaJiaSuWindow)UIPackage.CreateObject("UI_GuildDrill","GD_WeTaJiaSuWindow");
		}

		public UI_GD_WeTaJiaSuWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_petList = (GList)this.GetChildAt(2);
			m_close = (GButton)this.GetChildAt(3);
		}
	}
}