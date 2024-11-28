/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_GuideTip : GComponent
	{
		public GRichTextField m_tip;

		public const string URL = "ui://42sthz3tp9j2xqz";

		public static UI_GuideTip CreateInstance()
		{
			return (UI_GuideTip)UIPackage.CreateObject("UI_Common","GuideTip");
		}

		public UI_GuideTip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tip = (GRichTextField)this.GetChildAt(1);
		}
	}
}