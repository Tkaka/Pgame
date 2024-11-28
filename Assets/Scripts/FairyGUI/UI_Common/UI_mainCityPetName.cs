/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_mainCityPetName : GComponent
	{
		public GTextField m_nameLabel;

		public const string URL = "ui://42sthz3tsvtzxrc";

		public static UI_mainCityPetName CreateInstance()
		{
			return (UI_mainCityPetName)UIPackage.CreateObject("UI_Common","mainCityPetName");
		}

		public UI_mainCityPetName()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_nameLabel = (GTextField)this.GetChildAt(0);
		}
	}
}