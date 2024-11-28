/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_eMengBtn : GButton
	{
		public GImage m_redPoint;

		public const string URL = "ui://z04ymz0eeimk254";

		public static UI_eMengBtn CreateInstance()
		{
			return (UI_eMengBtn)UIPackage.CreateObject("UI_Level","eMengBtn");
		}

		public UI_eMengBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_redPoint = (GImage)this.GetChildAt(2);
		}
	}
}