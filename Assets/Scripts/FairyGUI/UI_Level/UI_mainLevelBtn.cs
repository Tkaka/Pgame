/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_mainLevelBtn : GButton
	{
		public GImage m_redPoint;

		public const string URL = "ui://z04ymz0e97kmn";

		public static UI_mainLevelBtn CreateInstance()
		{
			return (UI_mainLevelBtn)UIPackage.CreateObject("UI_Level","mainLevelBtn");
		}

		public UI_mainLevelBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_redPoint = (GImage)this.GetChildAt(2);
		}
	}
}