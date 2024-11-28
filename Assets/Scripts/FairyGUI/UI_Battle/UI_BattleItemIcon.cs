/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleItemIcon : GComponent
	{
		public GLoader m_borderLoader;
		public GLoader m_iconLoader;
		public GTextField m_numTxt;

		public const string URL = "ui://028ppdzhjkpz6s";

		public static UI_BattleItemIcon CreateInstance()
		{
			return (UI_BattleItemIcon)UIPackage.CreateObject("UI_Battle","BattleItemIcon");
		}

		public UI_BattleItemIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_numTxt = (GTextField)this.GetChildAt(2);
		}
	}
}