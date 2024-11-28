/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_mainCityHeadIcon : GComponent
	{
		public GLoader m_headIcon;

		public const string URL = "ui://jdfufi06d2tf7l";

		public static UI_mainCityHeadIcon CreateInstance()
		{
			return (UI_mainCityHeadIcon)UIPackage.CreateObject("UI_MainCity","mainCityHeadIcon");
		}

		public UI_mainCityHeadIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_headIcon = (GLoader)this.GetChildAt(0);
		}
	}
}