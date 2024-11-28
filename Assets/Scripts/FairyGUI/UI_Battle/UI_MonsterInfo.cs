/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_MonsterInfo : GComponent
	{
		public GTextField m_monsterName;

		public const string URL = "ui://028ppdzhnm275x";

		public static UI_MonsterInfo CreateInstance()
		{
			return (UI_MonsterInfo)UIPackage.CreateObject("UI_Battle","MonsterInfo");
		}

		public UI_MonsterInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_monsterName = (GTextField)this.GetChildAt(2);
		}
	}
}