/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_btnChallenge50 : GComponent
	{
		public GImage m_btnChallange50;

		public const string URL = "ui://3xs7lfyxh53e2h";

		public static UI_btnChallenge50 CreateInstance()
		{
			return (UI_btnChallenge50)UIPackage.CreateObject("UI_Arena","btnChallenge50");
		}

		public UI_btnChallenge50()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnChallange50 = (GImage)this.GetChildAt(0);
		}
	}
}