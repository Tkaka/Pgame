/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_btnClose : GComponent
	{
		public GImage m_closeBtn;

		public const string URL = "ui://0n5r1ymrl73xg";

		public static UI_btnClose CreateInstance()
		{
			return (UI_btnClose)UIPackage.CreateObject("UI_DailyActivity","btnClose");
		}

		public UI_btnClose()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GImage)this.GetChildAt(0);
		}
	}
}