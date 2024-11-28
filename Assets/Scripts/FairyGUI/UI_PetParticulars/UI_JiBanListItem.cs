/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_JiBanListItem : GComponent
	{
		public GTextField m_Name;
		public GTextField m_Content;

		public const string URL = "ui://rn5o3g4tfzr61";

		public static UI_JiBanListItem CreateInstance()
		{
			return (UI_JiBanListItem)UIPackage.CreateObject("UI_PetParticulars","JiBanListItem");
		}

		public UI_JiBanListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Name = (GTextField)this.GetChildAt(0);
			m_Content = (GTextField)this.GetChildAt(1);
		}
	}
}