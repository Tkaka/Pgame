/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_SaoDangCompleteCell : GComponent
	{
		public Transition m_anim;

		public const string URL = "ui://34cd5b4hh9lh2n";

		public static UI_SaoDangCompleteCell CreateInstance()
		{
			return (UI_SaoDangCompleteCell)UIPackage.CreateObject("UI_SaoDangJieSuan","SaoDangCompleteCell");
		}

		public UI_SaoDangCompleteCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_anim = this.GetTransitionAt(0);
		}
	}
}