/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_eiteLevelBtn : GButton
	{
		public GImage m_btnIcon;
		public GImage m_redPoint;
		public GImage m_lockIcon;

		public const string URL = "ui://z04ymz0e97kmo";

		public static UI_eiteLevelBtn CreateInstance()
		{
			return (UI_eiteLevelBtn)UIPackage.CreateObject("UI_Level","eiteLevelBtn");
		}

		public UI_eiteLevelBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnIcon = (GImage)this.GetChildAt(0);
			m_redPoint = (GImage)this.GetChildAt(2);
			m_lockIcon = (GImage)this.GetChildAt(3);
		}
	}
}