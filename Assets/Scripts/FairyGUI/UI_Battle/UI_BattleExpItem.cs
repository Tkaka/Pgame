/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleExpItem : GComponent
	{
		public UI_BattlePetIcon m_petIcon;
		public GTextField m_txtExp;
		public GGroup m_addExpG;
		public GGroup m_levelupG;
		public GTextField m_expFull;

		public const string URL = "ui://028ppdzhjkpz6q";

		public static UI_BattleExpItem CreateInstance()
		{
			return (UI_BattleExpItem)UIPackage.CreateObject("UI_Battle","BattleExpItem");
		}

		public UI_BattleExpItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_petIcon = (UI_BattlePetIcon)this.GetChildAt(0);
			m_txtExp = (GTextField)this.GetChildAt(2);
			m_addExpG = (GGroup)this.GetChildAt(3);
			m_levelupG = (GGroup)this.GetChildAt(6);
			m_expFull = (GTextField)this.GetChildAt(7);
		}
	}
}