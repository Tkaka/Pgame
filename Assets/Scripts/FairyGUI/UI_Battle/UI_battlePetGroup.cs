/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_battlePetGroup : GComponent
	{
		public GList m_petList;
		public GGroup m_petGroup;

		public const string URL = "ui://028ppdzhlkuwsh3";

		public static UI_battlePetGroup CreateInstance()
		{
			return (UI_battlePetGroup)UIPackage.CreateObject("UI_Battle","battlePetGroup");
		}

		public UI_battlePetGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_petList = (GList)this.GetChildAt(1);
			m_petGroup = (GGroup)this.GetChildAt(2);
		}
	}
}