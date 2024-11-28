/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BattleEnd
{
	public partial class UI_BattleEndChiBang : GComponent
	{
		public Transition m_t0;

		public const string URL = "ui://x8kuvx95sbfs6";

		public static UI_BattleEndChiBang CreateInstance()
		{
			return (UI_BattleEndChiBang)UIPackage.CreateObject("UI_BattleEnd","BattleEndChiBang");
		}

		public UI_BattleEndChiBang()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_t0 = this.GetTransitionAt(0);
		}
	}
}