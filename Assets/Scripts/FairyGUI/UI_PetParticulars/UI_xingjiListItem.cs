/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_xingjiListItem : GComponent
	{
		public GImage m_star;

		public const string URL = "ui://rn5o3g4t10f6hi";

		public static UI_xingjiListItem CreateInstance()
		{
			return (UI_xingjiListItem)UIPackage.CreateObject("UI_PetParticulars","xingjiListItem");
		}

		public UI_xingjiListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_star = (GImage)this.GetChildAt(0);
		}
	}
}