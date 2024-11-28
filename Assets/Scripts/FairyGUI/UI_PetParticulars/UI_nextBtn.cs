/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_nextBtn : GButton
	{
		public GImage m_next;
		public GGraph m_next_2;

		public const string URL = "ui://rn5o3g4twhkshn";

		public static UI_nextBtn CreateInstance()
		{
			return (UI_nextBtn)UIPackage.CreateObject("UI_PetParticulars","nextBtn");
		}

		public UI_nextBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_next = (GImage)this.GetChildAt(0);
			m_next_2 = (GGraph)this.GetChildAt(1);
		}
	}
}