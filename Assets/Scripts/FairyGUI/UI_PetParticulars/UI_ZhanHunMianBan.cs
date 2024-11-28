/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_ZhanHunMianBan : GComponent
	{
		public GList m_ZhanHunList;

		public const string URL = "ui://rn5o3g4tfzr6b";

		public static UI_ZhanHunMianBan CreateInstance()
		{
			return (UI_ZhanHunMianBan)UIPackage.CreateObject("UI_PetParticulars","ZhanHunMianBan");
		}

		public UI_ZhanHunMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ZhanHunList = (GList)this.GetChildAt(3);
		}
	}
}