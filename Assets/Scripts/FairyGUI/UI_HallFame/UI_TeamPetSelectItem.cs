/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_TeamPetSelectItem : GButton
	{
		public GLoader m_touXiang;

		public const string URL = "ui://yo5kunkirtv94";

		public static UI_TeamPetSelectItem CreateInstance()
		{
			return (UI_TeamPetSelectItem)UIPackage.CreateObject("UI_HallFame","TeamPetSelectItem");
		}

		public UI_TeamPetSelectItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_touXiang = (GLoader)this.GetChildAt(0);
		}
	}
}