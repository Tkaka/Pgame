/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_EquipBtn : GButton
	{
		public GImage m_lockGroup;

		public const string URL = "ui://4asqm7awe4jb58";

		public static UI_EquipBtn CreateInstance()
		{
			return (UI_EquipBtn)UIPackage.CreateObject("UI_GeDouJia","EquipBtn");
		}

		public UI_EquipBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lockGroup = (GImage)this.GetChildAt(2);
		}
	}
}