/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_strengthBtn : GButton
	{
		public GTextField m_title1;
		public GComponent m_lockGroup;

		public const string URL = "ui://8u3gv94nt5fa7";

		public static UI_strengthBtn CreateInstance()
		{
			return (UI_strengthBtn)UIPackage.CreateObject("UI_Equip","strengthBtn");
		}

		public UI_strengthBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title1 = (GTextField)this.GetChildAt(3);
			m_lockGroup = (GComponent)this.GetChildAt(4);
		}
	}
}