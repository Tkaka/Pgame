/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_btnScore : GComponent
	{
		public GImage m_imgScoreRed;

		public const string URL = "ui://3xs7lfyxo0der";

		public static UI_btnScore CreateInstance()
		{
			return (UI_btnScore)UIPackage.CreateObject("UI_Arena","btnScore");
		}

		public UI_btnScore()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgScoreRed = (GImage)this.GetChildAt(2);
		}
	}
}