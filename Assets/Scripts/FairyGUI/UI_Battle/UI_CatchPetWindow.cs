/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_CatchPetWindow : GComponent
	{
		public UI_MonsterInfo m_monsterInfo0;
		public UI_MonsterInfo m_monsterInfo1;
		public UI_MonsterInfo m_monsterInfo2;

		public const string URL = "ui://028ppdzhnm275u";

		public static UI_CatchPetWindow CreateInstance()
		{
			return (UI_CatchPetWindow)UIPackage.CreateObject("UI_Battle","CatchPetWindow");
		}

		public UI_CatchPetWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_monsterInfo0 = (UI_MonsterInfo)this.GetChildAt(1);
			m_monsterInfo1 = (UI_MonsterInfo)this.GetChildAt(2);
			m_monsterInfo2 = (UI_MonsterInfo)this.GetChildAt(3);
		}
	}
}