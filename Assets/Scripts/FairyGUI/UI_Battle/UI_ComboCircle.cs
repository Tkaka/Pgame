/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ComboCircle : GComponent
	{
		public GImage m_circle;
		public GImage m_center;
		public Transition m_scale;

		public const string URL = "ui://028ppdzhqe4t6b";

		public static UI_ComboCircle CreateInstance()
		{
			return (UI_ComboCircle)UIPackage.CreateObject("UI_Battle","ComboCircle");
		}

		public UI_ComboCircle()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_circle = (GImage)this.GetChildAt(0);
			m_center = (GImage)this.GetChildAt(2);
			m_scale = this.GetTransitionAt(0);
		}
	}
}