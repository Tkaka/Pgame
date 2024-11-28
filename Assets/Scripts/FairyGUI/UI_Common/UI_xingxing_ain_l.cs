/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_xingxing_ain_l : GComponent
	{
		public Transition m_anim_L;

		public const string URL = "ui://42sthz3tifu4xrg";

		public static UI_xingxing_ain_l CreateInstance()
		{
			return (UI_xingxing_ain_l)UIPackage.CreateObject("UI_Common","xingxing_ain_l");
		}

		public UI_xingxing_ain_l()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_anim_L = this.GetTransitionAt(0);
		}
	}
}