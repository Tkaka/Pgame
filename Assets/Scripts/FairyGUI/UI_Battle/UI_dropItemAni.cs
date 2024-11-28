/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_dropItemAni : GComponent
	{
		public GLoader m_imgLoader;
		public Transition m_dropAni;

		public const string URL = "ui://028ppdzhlkuwsh1";

		public static UI_dropItemAni CreateInstance()
		{
			return (UI_dropItemAni)UIPackage.CreateObject("UI_Battle","dropItemAni");
		}

		public UI_dropItemAni()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgLoader = (GLoader)this.GetChildAt(0);
			m_dropAni = this.GetTransitionAt(0);
		}
	}
}