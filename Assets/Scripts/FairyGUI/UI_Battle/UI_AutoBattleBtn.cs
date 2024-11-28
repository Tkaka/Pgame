/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_AutoBattleBtn : GButton
	{
		public GImage m_aniImg;
		public Transition m_ani;

		public const string URL = "ui://028ppdzhicla3p";

		public static UI_AutoBattleBtn CreateInstance()
		{
			return (UI_AutoBattleBtn)UIPackage.CreateObject("UI_Battle","AutoBattleBtn");
		}

		public UI_AutoBattleBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_aniImg = (GImage)this.GetChildAt(5);
			m_ani = this.GetTransitionAt(0);
		}
	}
}