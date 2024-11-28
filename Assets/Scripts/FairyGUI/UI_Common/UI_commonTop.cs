/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_commonTop : GComponent
	{
		public GButton m_closeBtn;
		public GTextField m_title;
		public Transition m_anim;

		public const string URL = "ui://42sthz3t8x27xn0";

		public static UI_commonTop CreateInstance()
		{
			return (UI_commonTop)UIPackage.CreateObject("UI_Common","commonTop");
		}

		public UI_commonTop()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GButton)this.GetChildAt(1);
			m_title = (GTextField)this.GetChildAt(2);
			m_anim = this.GetTransitionAt(0);
		}
	}
}