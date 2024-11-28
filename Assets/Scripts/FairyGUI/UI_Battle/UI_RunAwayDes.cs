/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_RunAwayDes : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://028ppdzhgqt1sh5";

		public static UI_RunAwayDes CreateInstance()
		{
			return (UI_RunAwayDes)UIPackage.CreateObject("UI_Battle","RunAwayDes");
		}

		public UI_RunAwayDes()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
		}
	}
}