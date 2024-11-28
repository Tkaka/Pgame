/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_EffectFont : GComponent
	{
		public GList m_list;
		public Transition m_ani;

		public const string URL = "ui://028ppdzhl2ir9p";

		public static UI_EffectFont CreateInstance()
		{
			return (UI_EffectFont)UIPackage.CreateObject("UI_Battle","EffectFont");
		}

		public UI_EffectFont()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_list = (GList)this.GetChildAt(0);
			m_ani = this.GetTransitionAt(0);
		}
	}
}