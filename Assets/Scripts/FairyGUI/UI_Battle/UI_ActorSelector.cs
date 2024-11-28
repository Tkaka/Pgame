/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ActorSelector : GComponent
	{
		public GImage m_cricle;
		public Transition m_t0;

		public const string URL = "ui://028ppdzhumw7f9";

		public static UI_ActorSelector CreateInstance()
		{
			return (UI_ActorSelector)UIPackage.CreateObject("UI_Battle","ActorSelector");
		}

		public UI_ActorSelector()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_cricle = (GImage)this.GetChildAt(1);
			m_t0 = this.GetTransitionAt(0);
		}
	}
}