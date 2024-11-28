/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objProgressBar3 : GProgressBar
	{
		public GTextField m_txtProgressBar;

		public const string URL = "ui://oe7ras64nos7b48";

		public static UI_objProgressBar3 CreateInstance()
		{
			return (UI_objProgressBar3)UIPackage.CreateObject("UI_Guild","objProgressBar3");
		}

		public UI_objProgressBar3()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtProgressBar = (GTextField)this.GetChildAt(2);
		}
	}
}