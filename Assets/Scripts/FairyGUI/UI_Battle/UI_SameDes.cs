/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_SameDes : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://028ppdzhgqt1sh4";

		public static UI_SameDes CreateInstance()
		{
			return (UI_SameDes)UIPackage.CreateObject("UI_Battle","SameDes");
		}

		public UI_SameDes()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(0);
		}
	}
}