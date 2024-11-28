/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_Player_Info : GComponent
	{
		public GTextField m_Level;
		public GTextField m_Name;
		public GTextField m_ZhanLi;

		public const string URL = "ui://jdfufi06ro1f6e";

		public static UI_Player_Info CreateInstance()
		{
			return (UI_Player_Info)UIPackage.CreateObject("UI_MainCity","Player_Info");
		}

		public UI_Player_Info()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Level = (GTextField)this.GetChildAt(3);
			m_Name = (GTextField)this.GetChildAt(4);
			m_ZhanLi = (GTextField)this.GetChildAt(7);
		}
	}
}