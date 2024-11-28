/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleStar : GComponent
	{
		public GImage m_star;
		public GTextField m_desc;

		public const string URL = "ui://028ppdzhjkpz6t";

		public static UI_BattleStar CreateInstance()
		{
			return (UI_BattleStar)UIPackage.CreateObject("UI_Battle","BattleStar");
		}

		public UI_BattleStar()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_star = (GImage)this.GetChildAt(1);
			m_desc = (GTextField)this.GetChildAt(3);
		}
	}
}