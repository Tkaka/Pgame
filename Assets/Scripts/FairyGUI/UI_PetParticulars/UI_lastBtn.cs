/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_lastBtn : GButton
	{
		public GGraph m_last;

		public const string URL = "ui://rn5o3g4twhkshm";

		public static UI_lastBtn CreateInstance()
		{
			return (UI_lastBtn)UIPackage.CreateObject("UI_PetParticulars","lastBtn");
		}

		public UI_lastBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_last = (GGraph)this.GetChildAt(1);
		}
	}
}