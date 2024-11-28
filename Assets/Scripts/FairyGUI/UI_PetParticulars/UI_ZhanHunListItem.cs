/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_ZhanHunListItem : GComponent
	{
		public GLoader m_touxiang;

		public const string URL = "ui://rn5o3g4tfzr64";

		public static UI_ZhanHunListItem CreateInstance()
		{
			return (UI_ZhanHunListItem)UIPackage.CreateObject("UI_PetParticulars","ZhanHunListItem");
		}

		public UI_ZhanHunListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_touxiang = (GLoader)this.GetChildAt(1);
		}
	}
}