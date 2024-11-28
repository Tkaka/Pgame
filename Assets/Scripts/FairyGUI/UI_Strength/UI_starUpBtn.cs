/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_starUpBtn : GButton
	{
		public GTextField m_title1;
		public GComponent m_lockGroup;

		public const string URL = "ui://qnd9tp35uxhi46";

		public static UI_starUpBtn CreateInstance()
		{
			return (UI_starUpBtn)UIPackage.CreateObject("UI_Strength","starUpBtn");
		}

		public UI_starUpBtn()
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