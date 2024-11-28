/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Achievement
{
	public partial class UI_AM_TitleWindow : GComponent
	{
		public GList m_TitleList;
		public GButton m_CloseBtn;

		public const string URL = "ui://xpd8f6j0qkz1j";

		public static UI_AM_TitleWindow CreateInstance()
		{
			return (UI_AM_TitleWindow)UIPackage.CreateObject("UI_Achievement","AM_TitleWindow");
		}

		public UI_AM_TitleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TitleList = (GList)this.GetChildAt(3);
			m_CloseBtn = (GButton)this.GetChildAt(4);
		}
	}
}