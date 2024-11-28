/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objProgressBar2 : GProgressBar
	{
		public GTextField m_txtProgress;

		public const string URL = "ui://oe7ras64lcbob40";

		public static UI_objProgressBar2 CreateInstance()
		{
			return (UI_objProgressBar2)UIPackage.CreateObject("UI_Guild","objProgressBar2");
		}

		public UI_objProgressBar2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtProgress = (GTextField)this.GetChildAt(2);
		}
	}
}