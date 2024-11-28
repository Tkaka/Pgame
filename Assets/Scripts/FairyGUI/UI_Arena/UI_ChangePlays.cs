/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ChangePlays : GComponent
	{
		public GComponent m_btnHuan;
		public GTextField m_txtNum;
		public GImage m_imgZhuanShi;

		public const string URL = "ui://3xs7lfyxo0de14";

		public static UI_ChangePlays CreateInstance()
		{
			return (UI_ChangePlays)UIPackage.CreateObject("UI_Arena","ChangePlays");
		}

		public UI_ChangePlays()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnHuan = (GComponent)this.GetChildAt(0);
			m_txtNum = (GTextField)this.GetChildAt(1);
			m_imgZhuanShi = (GImage)this.GetChildAt(2);
		}
	}
}