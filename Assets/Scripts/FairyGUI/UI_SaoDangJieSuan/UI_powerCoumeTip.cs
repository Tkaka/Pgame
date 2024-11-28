/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_powerCoumeTip : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://34cd5b4hjr2f2c";

		public static UI_powerCoumeTip CreateInstance()
		{
			return (UI_powerCoumeTip)UIPackage.CreateObject("UI_SaoDangJieSuan","powerCoumeTip");
		}

		public UI_powerCoumeTip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
		}
	}
}