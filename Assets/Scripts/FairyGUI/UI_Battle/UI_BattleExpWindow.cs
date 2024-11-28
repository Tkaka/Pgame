/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleExpWindow : GComponent
	{
		public GImage m_bgLeft;
		public GImage m_bgRight;
		public GList m_list;

		public const string URL = "ui://028ppdzhjkpz6n";

		public static UI_BattleExpWindow CreateInstance()
		{
			return (UI_BattleExpWindow)UIPackage.CreateObject("UI_Battle","BattleExpWindow");
		}

		public UI_BattleExpWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bgLeft = (GImage)this.GetChildAt(1);
			m_bgRight = (GImage)this.GetChildAt(2);
			m_list = (GList)this.GetChildAt(3);
		}
	}
}