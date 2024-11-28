/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_mainCityPetInfo : GComponent
	{
		public UI_StarList m_starList;
		public GLoader m_typeLoader;
		public GTextField m_levelLabel;

		public const string URL = "ui://42sthz3tyayxxrb";

		public static UI_mainCityPetInfo CreateInstance()
		{
			return (UI_mainCityPetInfo)UIPackage.CreateObject("UI_Common","mainCityPetInfo");
		}

		public UI_mainCityPetInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_starList = (UI_StarList)this.GetChildAt(0);
			m_typeLoader = (GLoader)this.GetChildAt(1);
			m_levelLabel = (GTextField)this.GetChildAt(2);
		}
	}
}