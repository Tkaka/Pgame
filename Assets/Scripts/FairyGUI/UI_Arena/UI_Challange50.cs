/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_Challange50 : GComponent
	{
		public UI_btnChallenge50 m_btnChallenge50;
		public GComponent m_btnChallange;

		public const string URL = "ui://3xs7lfyxo0de1c";

		public static UI_Challange50 CreateInstance()
		{
			return (UI_Challange50)UIPackage.CreateObject("UI_Arena","Challange50");
		}

		public UI_Challange50()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnChallenge50 = (UI_btnChallenge50)this.GetChildAt(0);
			m_btnChallange = (GComponent)this.GetChildAt(1);
		}
	}
}