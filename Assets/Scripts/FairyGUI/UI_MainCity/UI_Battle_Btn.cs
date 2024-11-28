/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_Battle_Btn : GButton
	{
		public Transition m_anim;

		public const string URL = "ui://jdfufi06ro1f60";

		public static UI_Battle_Btn CreateInstance()
		{
			return (UI_Battle_Btn)UIPackage.CreateObject("UI_MainCity","Battle_Btn");
		}

		public UI_Battle_Btn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_anim = this.GetTransitionAt(0);
		}
	}
}