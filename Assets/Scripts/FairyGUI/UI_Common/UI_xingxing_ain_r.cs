/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_xingxing_ain_r : GComponent
	{
		public Transition m_anim_R;

		public const string URL = "ui://42sthz3tifu4xrh";

		public static UI_xingxing_ain_r CreateInstance()
		{
			return (UI_xingxing_ain_r)UIPackage.CreateObject("UI_Common","xingxing_ain_r");
		}

		public UI_xingxing_ain_r()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_anim_R = this.GetTransitionAt(0);
		}
	}
}