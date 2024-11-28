/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_TipWindow : GComponent
	{
		public GTextField m_tipTxt;
		public GGroup m_cont;

		public const string URL = "ui://42sthz3tnc9egc";

		public static UI_TipWindow CreateInstance()
		{
			return (UI_TipWindow)UIPackage.CreateObject("UI_Common","TipWindow");
		}

		public UI_TipWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipTxt = (GTextField)this.GetChildAt(1);
			m_cont = (GGroup)this.GetChildAt(2);
		}
	}
}