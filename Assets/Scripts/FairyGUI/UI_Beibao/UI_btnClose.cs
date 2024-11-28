/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_btnClose : GComponent
	{
		public GImage m_closeBtn;

		public const string URL = "ui://g5pgln3n12m7e41";

		public static UI_btnClose CreateInstance()
		{
			return (UI_btnClose)UIPackage.CreateObject("UI_Beibao","btnClose");
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