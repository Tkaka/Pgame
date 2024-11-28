/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_SBg : GButton
	{
		public Controller m_c1;
		public GImage m_imgBG;

		public const string URL = "ui://g5pgln3n12m7e49";

		public static UI_SBg CreateInstance()
		{
			return (UI_SBg)UIPackage.CreateObject("UI_Beibao","SBg");
		}

		public UI_SBg()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
			m_imgBG = (GImage)this.GetChildAt(0);
		}
	}
}