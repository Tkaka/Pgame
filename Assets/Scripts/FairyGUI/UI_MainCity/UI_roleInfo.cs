/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_roleInfo : GComponent
	{
		public UI_mainCityHeadIcon m_mainCityHeadIcon;
		public GTextField m_Level;
		public GTextField m_Name;
		public GTextField m_ZhanLi;
		public GComponent m_headIcon;
		public GGraph m_headTouch;
		public Transition m_anim;

		public const string URL = "ui://jdfufi06npep7i";

		public static UI_roleInfo CreateInstance()
		{
			return (UI_roleInfo)UIPackage.CreateObject("UI_MainCity","roleInfo");
		}

		public UI_roleInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mainCityHeadIcon = (UI_mainCityHeadIcon)this.GetChildAt(1);
			m_Level = (GTextField)this.GetChildAt(3);
			m_Name = (GTextField)this.GetChildAt(4);
			m_ZhanLi = (GTextField)this.GetChildAt(6);
			m_headIcon = (GComponent)this.GetChildAt(7);
			m_headTouch = (GGraph)this.GetChildAt(8);
			m_anim = this.GetTransitionAt(0);
		}
	}
}