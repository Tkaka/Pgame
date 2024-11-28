/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_HurtNumberWrapper : GComponent
	{
		public GComponent m_hurtNumber;
		public Transition m_ani;
		public Transition m_hongqiu;

		public const string URL = "ui://028ppdzhicla20";

		public static UI_HurtNumberWrapper CreateInstance()
		{
			return (UI_HurtNumberWrapper)UIPackage.CreateObject("UI_Battle","HurtNumberWrapper");
		}

		public UI_HurtNumberWrapper()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_hurtNumber = (GComponent)this.GetChildAt(0);
			m_ani = this.GetTransitionAt(0);
			m_hongqiu = this.GetTransitionAt(1);
		}
	}
}